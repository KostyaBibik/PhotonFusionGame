using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Game.Core.Installers;
using UnityEngine;

namespace Game.Core.Components
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 MovementInput;
    }
    
    public class PlayerInputComponent : NetworkBehaviour, INetworkRunnerCallbacks, IPlayerLeft
    {
        private InputHandler _inputHandler; 
        private AxisInputContext _movementContext;
        private Vector3 _movementInput;
        private MovementComponent _movementComponent;
        private NetworkRunner _networkRunner;

        private bool _aliveStatus;
        
        private void Awake()
        {
            _networkRunner = DiContainerRef.Container.Resolve<NetworkRunner>();
            _inputHandler = DiContainerRef.Container.Resolve<InputHandler>();
            
            _movementContext = _inputHandler.GetContext<MovementContext>();
        }

        private void Start()
        {
            _networkRunner.AddCallbacks(this);
        }

        public override void FixedUpdateNetwork()
        {
            if(_aliveStatus) 
                return;
            
            if(Object == null)
                return;
            
            if(!Object.InputAuthority)
                return;
            
            _movementInput = new Vector3(_movementContext.Value.x, 0f, _movementContext.Value.y);
        }
        
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if(_aliveStatus) 
                return;
            
            if(Object == null)
                return;
            
            if(!Object.InputAuthority)
                return;
            
            NetworkInputData inputData = new NetworkInputData
            {
                MovementInput = _movementInput
            };
            
            input.Set(inputData);
        }

        public void PlayerLeft(PlayerRef player)
        {
            if(!Object.InputAuthority)
                return;

            var playerObject = _networkRunner.GetPlayerObject(player);
            
            if (playerObject == Object)
            {
                _aliveStatus = true;
                _networkRunner.Despawn(Object);
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

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

        public void OnSceneLoadDone(NetworkRunner runner) { }
    }
}