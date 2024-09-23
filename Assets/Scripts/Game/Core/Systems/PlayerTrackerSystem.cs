using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Game.Core.Factories.Impl;
using UnityEngine;
using Zenject;

namespace Game.Core.Systems
{
    public class PlayerTrackerSystem : INetworkRunnerCallbacks, IInitializable
    {
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private readonly NetworkRunner _networkRunner;

        public void Initialize()
        {
            _networkRunner.AddCallbacks(this);
        }
        
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (_networkRunner.IsServer)
            {
                _playerFactory.Create(new Vector3(0, 0, 0), player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            /*if (_networkRunner.IsServer)
            {
                _playerFactory.Create(new Vector3(0, 0, 0));
            }*/
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            //Debug.Log("OnInput");
        }

#region Unused Callbacks from Network

        public void OnConnectedToServer(NetworkRunner runner) {}
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
        public void OnDisconnectedFromServer(NetworkRunner runner) {}
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
        public void OnPlayerActivation(NetworkRunner runner, PlayerRef player) {}
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {}
        public void OnSceneLoadDone(NetworkRunner runner) {}
        public void OnSceneLoadStart(NetworkRunner runner) {}
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}

#endregion

    }
}