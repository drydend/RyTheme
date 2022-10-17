using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class StoryLevelLoaderInstaler : MonoInstaller
    {
        [SerializeField]
        private StoryLevelLoader _storyLevelLoaderPrefab;

        public override void InstallBindings()
        {
            Container.Bind<StoryLevelLoader>()
                .FromComponentInNewPrefab(_storyLevelLoaderPrefab)
                .AsSingle();
        }

    }
}
