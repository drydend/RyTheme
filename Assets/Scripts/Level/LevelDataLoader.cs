using ModestTree;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataLoader
{
    private const string PathToSongsDirectory = @"\Resources\Songs\";
    private const string StorySongsDirectoryName = @"StorySongs";
    private const string OtherSongsDirectoryName = @"Other";

    private static Dictionary<string, LevelData> _loadedLevels = new Dictionary<string, LevelData>();

    private ParsedTrackData _parsedTrackData;
    private LevelType _levelType;
    private LoadingSongType _loadingSongType;
    private string _smFileName;

    public LevelDataLoader(string smFileName, LevelType levelType, LoadingSongType loadingSongType)
    {
        _smFileName = smFileName;
        _levelType = levelType;
        _loadingSongType = loadingSongType;
    }

    public LevelDataLoader(ParsedTrackData parsedTrackData, LevelType levelType, LoadingSongType loadingSongType)
    {
        _parsedTrackData = parsedTrackData;
        _smFileName = parsedTrackData.SmFileName;
        _levelType = levelType;
        _loadingSongType = loadingSongType;
    }

    public LevelData GetData()
    {
        string songDirectory;
        var smFilePath = GetSMFilePath(GetSongsDirectoryPath(), out songDirectory);

        if (_loadedLevels.ContainsKey(smFilePath))
        {
            return _loadedLevels[smFilePath];
        }

        if (_parsedTrackData == null)
        {
            var lines = File.ReadAllLines(smFilePath);
            var parser = new SmParser(lines, _smFileName);
            _parsedTrackData = parser.Parse();
        }

        var bannerPath = GetResourcePath(songDirectory + @"/" +
            GetFileNameWithoutExtinsion(_parsedTrackData.BannerName));
        var backgroundPath = GetResourcePath(songDirectory + @"/" +
            GetFileNameWithoutExtinsion(_parsedTrackData.BackgroundName));
        var musicResourcePath = GetResourcePath(songDirectory + @"/" +
            GetFileNameWithoutExtinsion(_parsedTrackData.Music));

        Sprite banner = Resources.Load<Sprite>(bannerPath);
        Sprite background = Resources.Load<Sprite>(backgroundPath);
        AudioClip music = Resources.Load<AudioClip>(musicResourcePath);

        var levelData = new LevelData(_parsedTrackData, banner, background, music, _levelType);
        _loadedLevels[smFilePath] = levelData;

        return levelData;
    }

    private string GetSongsDirectoryPath()
    {
        switch (_loadingSongType)
        {
            case LoadingSongType.Story:
                return Application.dataPath + PathToSongsDirectory + StorySongsDirectoryName;
            case LoadingSongType.Other:
                return Application.dataPath + PathToSongsDirectory + OtherSongsDirectoryName;
            default:
                return string.Empty;
        }
    }

    private string GetSMFilePath(string songsDirectoryPath, out string directoryPath)
    {
        foreach (var directory in Directory.GetDirectories(songsDirectoryPath))
        {
            foreach (var filePath in Directory.GetFiles(directory))
            {
                if (filePath.EndsWith(_smFileName))
                {
                    directoryPath = directory;
                    return filePath;
                }
            }
        }

        throw new System.Exception($"There is no sm file (name:{_smFileName})" +
            $" in directory (directory path{songsDirectoryPath})");
    }

    private string GetResourcePath(string absolutePath)
    {
        var directories = absolutePath.Split('/', '\\');

        for (int i = 0; i < directories.Length; i++)
        {
            if (directories[i] == "Resources")
            {
                string[] resourceDirectory = new ArraySegment<string>(directories, i + 1, directories.Length - i - 1)
                    .ToArray();
                var resourcePath = resourceDirectory.Join(@"/");

                return resourcePath;
            }
        }

        throw new Exception($"There is no resources directory in path {absolutePath}");
    }

    private string GetFileNameWithoutExtinsion(string name)
    {
        return name.Split('.')[0];
    }
}
