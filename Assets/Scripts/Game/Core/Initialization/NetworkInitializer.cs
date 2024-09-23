using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core.Initialization
{
    public class NetworkInitializer
    {
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly NetworkSceneManagerDefault _networkSceneManager;
        
        private int _sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Game.unity");
        private const string _sessionName = "MyRoomName";

        public async UniTask ConnectOrCreateRoom()
        {
            await StartGame();
        }
        
        private async UniTask StartGame()
        {
            _networkRunner.ProvideInput = true;
            
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = _sessionName,
                Scene = _sceneIndex,
                SceneManager = _networkSceneManager
            });
        }
    }
}