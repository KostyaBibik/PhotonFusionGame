using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Scenes
{
    public class SceneLoader
    {
        private const string GAME_SCENE = "Game";
        private const string EMPTY_SCENE = "Empty";

        public IReadOnlyReactiveProperty<EScene> CurrentScene => _currentScene;

        private ReactiveProperty<EScene> _currentScene = new(EScene.Boot);
        
        [Inject] private ZenjectSceneLoader _sceneLoader;

        public async UniTask LoadGame(
            Action<float> progressCallback = null,
            float sceneActivationDelay = 0
        )
        {
            if (_currentScene.Value == EScene.Game)
            {
                Debug.LogError($"Already on {EScene.Game} Scene");
                return;
            }

            await LoadSceneWithProgress(EScene.Game, GAME_SCENE, sceneActivationDelay, progressCallback);
        }
        
        private async UniTask LoadSceneWithProgress(
            EScene scene,
            string sceneName,
            float sceneActivationDelay,
            Action<float> progressCallback
        )
        {
            progressCallback(0);
			
            await _sceneLoader.LoadSceneAsync(EMPTY_SCENE)
                .AsObservable(progress: new Progress<float>(pr => progressCallback(pr * 0.2f)));

            var operation = _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            operation.allowSceneActivation = false;

            var fake = FakeProgress(pr => progressCallback(0.2f + 0.6f * pr), 0.5f);

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    break;
                }
                
                await UniTask.NextFrame();
            }

            await fake;
            progressCallback(1);

            if (sceneActivationDelay > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(sceneActivationDelay));
            }
            
            _currentScene.Value = scene;
            operation.allowSceneActivation = true;
            await operation;
        }
        
        private async UniTask FakeProgress(Action<float> progress, float duration)
        {
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                progress(Mathf.Clamp01(time / duration));
                await UniTask.NextFrame();
            }
        }
    }
}