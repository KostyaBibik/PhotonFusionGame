using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.Scenes;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core.Initialization
{
    public class NetworkInitializer
    {
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly ProgressTrackingNetworkSceneManager _progressTrackingNetwork;
        
        private int _sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Game.unity");
        private const string _sessionName = "MyRoomName";

        public async UniTask ConnectOrCreateRoom(Action<float> onProgress)
        {
            _progressTrackingNetwork.OnSceneLoadProgress += onProgress;
            
            await StartGame();  
            
            _progressTrackingNetwork.OnSceneLoadProgress -= onProgress;
        }
        
        private async UniTask StartGame()
        {
            _networkRunner.ProvideInput = true;
            
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = _sessionName,
                Scene = _sceneIndex,
                SceneManager = _progressTrackingNetwork
            });
        }
    }
}