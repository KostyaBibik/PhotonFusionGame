using System;
using System.Collections.Generic;
using DataBase.Configs.Enemy;
using Fusion;
using Fusion.Sockets;
using Game.Core.Entities.EnemyImpl;
using Game.Core.Factories.Impl;
using Zenject;

namespace Game.Core.Systems.Enemy
{
    public class EnemySpawnSystem : IInitializable, INetworkRunnerCallbacks
    {
        [Inject] private readonly EnemyFactory _enemyFactory;
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly EnemyDataConfig _enemyDataConfig;

        public void Initialize()
        {
            if (_networkRunner.IsServer)
            {
                _networkRunner.AddCallbacks(this);
            }
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            if (_networkRunner.IsServer)
            {
                CreateInitialEnemies();
            }
        }
        
        private void CreateInitialEnemies()
        {
            var initialEnemies = _enemyDataConfig.InitialEnemies;
            foreach (var enemyGrade in initialEnemies)
            {
                SpawnEnemy(enemyGrade);
            }
        }

        public void SpawnEnemy(EEnemyGrade grade)
        {
            _enemyFactory.Create(grade);
        }
        
        #region Unused Callbacks from Network
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnConnectedToServer(NetworkRunner runner) { }

        public void OnDisconnectedFromServer(NetworkRunner runner) { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }
        
        #endregion
    }
}