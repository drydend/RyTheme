using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const string PathToSMDirectory = @"/Resources/LevelPatterns/SMFiles/";

    [SerializeField]
    private GameConfig _config;

    [SerializeField]
    private AudioPlayer _audioPlayer;
    [SerializeField]
    private Track _track;
    [SerializeField]
    private List<NotesLine> _notesLines;
    [SerializeField]
    private Note _notePrefab;

    private Level _currentLevel;
    private List<NoteSpawner> _notesSpawners = new List<NoteSpawner>();

    private void Start()
    {   
        var pathToSMFile = Application.dataPath + PathToSMDirectory + _track.SMFileName;
        Debug.Log(Application.dataPath);
        var levelPattern = new SmParser(ReadSMFile(pathToSMFile)).Parse();

        foreach (var line in _notesLines)
        {
            _notesSpawners.Add(new NoteSpawner(line, _notePrefab, _config.NoteTimeToCrossing));
        }

        _audioPlayer.Initialize(_config.NoteTimeToCrossing, _track, levelPattern.TrackData);

        _currentLevel = new Level(_audioPlayer, _track, levelPattern, _notesSpawners
            ,SupportedSongType.DanceSingle ,_config.NoteTimeToCrossing);
        _currentLevel.Start();
    }

    private string[] ReadSMFile(string path)
    {
        return File.ReadAllLines(path);
    }
}
