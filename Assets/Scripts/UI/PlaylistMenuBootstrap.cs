using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlaylistMenuBootstrap : MonoBehaviour
    {
        private const string TracksDirectory = "/Resources/Songs/Other";

        [SerializeField]
        private ChainMenuPlaylistItem _menuItemPrefab;
        [SerializeField]
        private ChainMenuConfig _menuConfig;
        [SerializeField]
        private RectTransform _itemsParent;
        [SerializeField]
        private PlayListMenuHandler _playListMenuHandler;


        private void Awake()
        {
            var smFiles = GetAllSmFiles();
            var parsedSongsData = GetAllParsedData(smFiles);
            var menuItems = CreateAllMenuItems(parsedSongsData);

            var chainMenu = new ChainMenu<ChainMenuPlaylistItem>(menuItems, _itemsParent, _menuConfig);
            chainMenu.SetItemsStartPosition();

            _playListMenuHandler.Initialize(chainMenu);
        }

        private List<ChainMenuPlaylistItem> CreateAllMenuItems(List<ParsedTrackData> tracksData)
        {
            var result = new List<ChainMenuPlaylistItem>();

            foreach (var item in tracksData)
            {
                foreach (var keyValuePair in item.LevelPatterns)
                {
                    var menuItem = Instantiate(_menuItemPrefab, _itemsParent);
                    menuItem.Initialize(item, keyValuePair.Key);

                    result.Add(menuItem);
                }
            }

            return result;
        }

        private string GetFileNameOnPath(string path)
        {
            return Path.GetFileName(path);
        }

        private List<ParsedTrackData> GetAllParsedData(List<string> smFiles)
        {
            var parsedSongsData = new List<ParsedTrackData>();

            foreach (var filePath in smFiles)
            {
                var parser = new SmParser(File.ReadAllLines(filePath), GetFileNameOnPath(filePath));
                parsedSongsData.Add(parser.Parse()); 
            }

            return parsedSongsData;
        }

        private List<string> GetAllSmFiles()
        {
            List<string> smFiles = new List<string>();

            foreach (var directories in Directory.GetDirectories(Application.dataPath + TracksDirectory))
            {
                foreach (var file in Directory.GetFiles(directories))
                {
                    if (file.EndsWith(".sm"))
                    {
                        smFiles.Add(file);
                    }
                }
            }

            return smFiles;
        }
    }
}