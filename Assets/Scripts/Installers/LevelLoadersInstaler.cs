using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class LevelLoadersInstaler : MonoInstaller
    {
        [SerializeField]
        private StoryLevelLoader _storyLevelLoaderPrefab;
        [SerializeField]
        private PlaylistLevelLoader _playlistLevelLoaderPrefab;

        public override void InstallBindings()
        {
            Container.Bind<StoryLevelLoader>()
                .FromComponentInNewPrefab(_storyLevelLoaderPrefab)
                .AsSingle();

            Container.Bind<PlaylistLevelLoader>()
                .FromComponentInNewPrefab(_playlistLevelLoaderPrefab)
                .AsSingle();
        }
    }
}
