﻿using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Game.Core.Factories.Impl;
using Game.Core.Services;
using Zenject;

namespace Game.Core.Systems
{
    public class PlayerTrackerSystem : INetworkRunnerCallbacks, IInitializable
    {
        [Inject] private readonly PlayerFactory _playerFactory;
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly PlayersService _playersService;

        public void Initialize()
        {
            if (_networkRunner.IsServer)
            {
                _networkRunner.AddCallbacks(this);
            }
        }
        
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (_networkRunner.IsServer)
            {
                _playerFactory.Create(player);
            }
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
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnSceneLoadStart(NetworkRunner runner) {}
        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}

#endregion

    }
}