using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class SmParser
{
    private const int BeatsInMeasure = 4;

    private float _songOffset;
    
    private List<Tuple<float, float>> _beatToBPM = new List<Tuple<float, float>>();

    private string[] _lines;
    private List<SupportedSongType> _supportedLevelTypes = new List<SupportedSongType>();

    private Dictionary<SupportedSongType,Queue<NoteData>> _notesData = new Dictionary<SupportedSongType,Queue<NoteData>>();
    
    public SmParser(string[] lines)
    {
        _supportedLevelTypes.Add(SupportedSongType.DanceSingle);
        _lines = lines;
    }

    public LevelPattern Parse()
    {
        for (int lineIndex = 0; lineIndex < _lines.Length; lineIndex++)
        {   
            var currentLine = _lines[lineIndex];

            if (currentLine.StartsWith("#OFFSET:"))
            {
                ParseOffset(currentLine);
            }
            else if (currentLine.StartsWith("#BPMS:"))
            {   
                var indexOfEndOfParameter = FindIndexOfEndOfParameter(lineIndex);
                ParseBPMS(lineIndex, indexOfEndOfParameter);
                lineIndex = indexOfEndOfParameter;
            }
            else if (currentLine.StartsWith("#NOTES:"))
            {
                var indexOfEndOfParameter = FindIndexOfEndOfParameter(lineIndex);
                ParseNotes(lineIndex, indexOfEndOfParameter);
                lineIndex = indexOfEndOfParameter;
            }
        }

        var trackData = new TrackData("Name", _songOffset);

        return new LevelPattern(_notesData, trackData);
    }

    private void ParseOffset(string line)
    {
        string subString = line.Substring(8, line.Length - 9);
        _songOffset = float.Parse(subString, CultureInfo.InvariantCulture);
    }

    private void ParseBPMS(int lineIndex, int indexOfEndOfParameter)
    {
        if (_lines[lineIndex].EndsWith(';'))
        {
            string equation = _lines[lineIndex].Substring(6, _lines[lineIndex].Length - 7);
            CreateAndRegisterBPMOfBeat(equation);
        }
        else
        {
            string equation = _lines[lineIndex].Substring(6);
            CreateAndRegisterBPMOfBeat(equation);

            for (int i = lineIndex + 1; i < indexOfEndOfParameter; i++)
            {
                equation = _lines[lineIndex].Substring(1);
                CreateAndRegisterBPMOfBeat(equation);
            }
        }
    }

    private void ParseNotes(int lineIndex, int indexOfEndOfParameter)
    {
        string songTypeString = _lines[lineIndex + 1].Trim(' ', ':').Replace("-", "").ToLower();

        if (!IsSongTypeSupported(songTypeString))
        {
            UnityEngine.Debug.LogError($"This song type is not supported {songTypeString}");
            return;
        }

        var songType = DefineSongType(songTypeString);

        switch (songType)
        {
            case SupportedSongType.DanceSingle:
                _notesData[SupportedSongType.DanceSingle] = ParseDanceSingle(lineIndex + 5, indexOfEndOfParameter);
                break;
            default:
                break;
        }

    }

    private Queue<NoteData> ParseDanceSingle(int lineIndex, int indexOfEndOfParameter)
    {
        var notesData = new Queue<NoteData>();

        int currentMeasure = 0;

        for (int i = lineIndex + 1; i < indexOfEndOfParameter; i++)
        {
            var indexOfEndOfMeasure = FindIndexOfEndOfMeasure(i);

            ArraySegment<string> measure = new ArraySegment<string>(_lines, i, indexOfEndOfMeasure - i);

            ParseMeasure(measure, notesData, currentMeasure);

            currentMeasure++;
            i = indexOfEndOfMeasure;
        }


        return notesData;
    }

    private void CreateAndRegisterBPMOfBeat(string equation)
    {
        string[] values = equation.Split('=');
        var bpmOfBeat = Tuple.Create(float.Parse(values[0], CultureInfo.InvariantCulture)
            , float.Parse(values[1], CultureInfo.InvariantCulture));
        _beatToBPM.Add(bpmOfBeat);
    }
    
    private void ParseMeasure(ArraySegment<string> array, Queue<NoteData> notesData, int currentMeasure)
    {
        float snap = array.Count;

        for (int i = 0; i < array.Count; i++)
        {
            float currentBeat = BeatsInMeasure * currentMeasure + BeatsInMeasure / snap * i;
            float pressTime = CalculatePressTimeOfBeat(currentBeat);

            for (int j = 0; j < array[i].Length; j++)
            {
                if (array[i][j] == '1')
                {
                    var noteData = new NoteData(pressTime, j);
                    notesData.Enqueue(noteData);
                }
            }
        }
    }

    private float CalculatePressTimeOfBeat(float beat)
    {   
        if (_beatToBPM.Count == 1)
        {
            return beat * 60 / _beatToBPM[0].Item2;
        }
        
        float pressTime = 0;

        for (int i = 0; i < _beatToBPM.Count - 1; i++)
        {
            if (_beatToBPM[i].Item1 <= beat && _beatToBPM[i + 1].Item1 <= beat)
            {
                pressTime += (_beatToBPM[i + 1].Item1 - _beatToBPM[i].Item1) * 60 / _beatToBPM[i].Item2;
            }
        }

        for (int i = 0; i < _beatToBPM.Count - 1; i++)
        {
            if (_beatToBPM[i].Item1 <= beat && _beatToBPM[i + 1].Item1 >= beat)
            {
                pressTime += (beat - _beatToBPM[i].Item1) * 60 / _beatToBPM[i].Item2;
            }
        }

        if(_beatToBPM.Last().Item1 < beat)
        {
            pressTime += (beat - _beatToBPM.Last().Item1) * 60 / _beatToBPM.Last().Item2;
        }

        return pressTime - _songOffset;
    }

    private bool IsSongTypeSupported(string songType)
    {
        foreach(var levelType in _supportedLevelTypes)
        {
            if (levelType.ToString().ToLower() == songType)
            {
                return true;
            }
        }

        return false;
    }
    private SupportedSongType DefineSongType(string songType)
    {
        foreach (var levelType in _supportedLevelTypes)
        {
            if (levelType.ToString().ToLower() == songType)
            {
                return levelType;
            }
        }

        throw new Exception($"Can`t define song type {songType}");
    }

    private int FindIndexOfEndOfParameter(int startIndex)
    {
        for (int i = startIndex; i < _lines.Length; i++)
        {
            if (_lines[i].EndsWith(';'))
            {
                return i;
            }
        }

        throw new Exception($"There is no end of parameter. \n Parameter starts at {startIndex}.");
    }

    private int FindIndexOfEndOfMeasure(int startIndex)
    {
        for (int i = startIndex; i < _lines.Length; i++)
        {
            if (_lines[i].EndsWith(',') || _lines[i].EndsWith(';'))
            {
                return i;
            }
        }

        throw new Exception($"There is no end of measure. \n Measure starts at {startIndex}.");
    }
}
