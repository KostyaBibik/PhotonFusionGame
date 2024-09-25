using Fusion;
using Game.Core.Initialization;
using Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Infrastructure.Boot
{
    public class Bootstrap : IInitializable
    {
        [Inject] private readonly BootstrapView _view;
        [Inject] private readonly SceneLoader _loader;
        [Inject] private readonly NetworkInitializer _networkInitializer;
        [Inject] private readonly NetworkSceneManagerDefault _networkSceneManager;

        public async void Initialize()
        {
            void ApplyProgress(float progress) =>
                _view.SetProgress(progress);
            
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            ApplyProgress(0);

            await _networkInitializer.ConnectOrCreateRoom(ApplyProgress);
            
           // await _loader.LoadGame(ApplyProgress);

            ApplyProgress(1);
        }
    }
}