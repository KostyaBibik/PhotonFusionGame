using Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Infrastructure.Boot
{
    public class Bootstrap : IInitializable
    {
        [Inject] private readonly BootstrapView _view;
        [Inject] private readonly SceneLoader _loader;

        public async void Initialize()
        {
            void ApplyProgress(float progress) =>
                _view.SetProgress(progress);
            
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            ApplyProgress(0);

            await _loader.LoadGame(ApplyProgress);

            ApplyProgress(1);
        }
    }
}