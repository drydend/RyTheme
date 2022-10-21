using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class SmParser
{
    private const int BeatsInMeasure = 4;

    
    private List<Tuple<float, float>> _beatToBPM = new List<Tuple<float, float>>();

    private string[] _lines;

    private string _title = "";
    private string _artist = "";
    private string _genre = "";
    private string _music = "";
    private string _bannerName = "";
    private string _backgroundName = "";
    private string _BPM = "";
    private float _songOffset;
    private Dictionary<LevelType,Queue<NoteData>> _notesData = new Dictionary<LevelType,Queue<NoteData>>();
    private Dictionary<LevelType,LevelDifficulty> _levelDifficulties = new Dictionary<LevelType, LevelDifficulty>();
    
    public SmParser(string[] lines)
    {
        _lines = lines;
    }

    public ParsedTrackData Parse()
    {
        for (int lineIndex = 0; lineIndex < _lines.Length; lineIndex++)
        {   
            var currentLine = _lines[lineIndex];

            if (currentLine.StartsWith("#TITLE:"))
            {
                ParseTitle(currentLine);
            }
            if (currentLine.StartsWith("#ARTIST:"))
            {
                ParseArtist(currentLine);
            }
            if (currentLine.StartsWith("#GENRE:"))
            {
                ParseGenre(currentLine);
            }
            if (currentLine.StartsWith("#MUSIC:"))
            {
                ParseMusic(currentLine);
            }
            if (currentLine.StartsWith("#BANNER:"))
            {
                ParseBanner(currentLine);
            }
            if (currentLine.StartsWith("#BACKGROUND:"))
            {
                ParseBackground(currentLine);
            }
            if (currentLine.StartsWith("#OFFSET:"))
            {
                ParseOffset(currentLine);
            }
            if (currentLine.StartsWith("#BPMS:"))
            {   
                var indexOfEndOfParameter = FindIndexOfEndOfParameter(lineIndex);
                ParseBPMS(lineIndex, indexOfEndOfParameter);
                lineIndex = indexOfEndOfParameter;
            }
            if (currentLine.StartsWith("#NOTES:"))
            {
                var indexOfEndOfParameter = FindIndexOfEndOfParameter(lineIndex);
                ParseNotes(lineIndex, indexOfEndOfParameter);
                lineIndex = indexOfEndOfParameter;
            }
        }

        var trackData = new ParsedTrackData(_title, _artist, _genre ,_bannerName, 
            _backgroundName, _music, _BPM, _songOffset,_levelDifficulties ,_notesData);

        return trackData;
    }


    private void ParseTitle(string currentLine)
    {
        _title = currentLine.Substring(7, currentLine.Length - 8);
    }

    private void ParseArtist(string currentLine)
    {
        _artist = currentLine.Substring(8, currentLine.Length - 9);
    }

    private void ParseGenre(string currentLine)
    {
        _genre = currentLine.Substring(7, currentLine.Length - 8);
    }

    private void ParseMusic(string currentLine)
    {
        _music = currentLine.Substring(7, currentLine.Length - 8);
    }
    private void ParseBanner(string currentLine)
    {
        _bannerName = currentLine.Substring(8, currentLine.Length - 9);
    } 
    private void ParseBackground(string currentLine)
    {
        _backgroundName = currentLine.Substring(12, currentLine.Length - 13);
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

        var lowestBPM = _beatToBPM.Select(turple => turple.Item2).Min();
        var highestBPM = _beatToBPM.Select(turple => turple.Item2).Max();

        _BPM = $"{(int)lowestBPM} - {(int)highestBPM}";
    }

    private void ParseNotes(int lineIndex, int indexOfEndOfParameter)
    {
        string songTypeString = _lines[lineIndex + 1].Trim(' ', ':').ToLower();
        string songDifficultyString = _lines[lineIndex + 3].Trim(' ', ':').Replace("-", "").ToLower();

        var songType = DefineSongType(songTypeString);
        var songDifficulty = DefineSongDifficulty(songDifficultyString);

        switch (songType)
        {
            case LevelType.Piano:
                _notesData[LevelType.Piano] = ParsePianoOrCross(lineIndex + 5, indexOfEndOfParameter);
                _levelDifficulties[LevelType.Piano] = songDifficulty;
                break;
            case LevelType.Cross:
                _notesData[LevelType.Cross] = ParsePianoOrCross(lineIndex + 5, indexOfEndOfParameter);
                _levelDifficulties[LevelType.Cross] = songDifficulty;
                break;
            default:
                break;
        }

    }

    private Queue<NoteData> ParsePianoOrCross(int lineIndex, int indexOfEndOfParameter)
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

    private LevelType DefineSongType(string songType)
    {
        switch (songType)
        {
            case "dance-piano":
                return LevelType.Piano;
            case "dance-cross":
                return LevelType.Cross;
            default:
                break;
        }

        throw new Exception($"Can`t define song type {songType}");
    }

    private LevelDifficulty DefineSongDifficulty(string songDifficultyString)
    {
        foreach (var levelType in Enum.GetNames(typeof(LevelDifficulty)))
        {
            if (levelType.ToString().ToLower() == songDifficultyString)
            {
                return Enum.Parse<LevelDifficulty>(levelType, true);
            }
        }

        throw new Exception($"Can`t define song type {songDifficultyString}");
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
