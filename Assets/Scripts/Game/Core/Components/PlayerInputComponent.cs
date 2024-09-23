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
        public Vector3 movementInput;
    }
    
    public class PlayerInputComponent : NetworkBehaviour, INetworkRunnerCallbacks
    {
        private InputHandler _inputHandler; 
        private AxisInputContext _movementContext;
        private Vector3 _movementInput;
        private MovementComponent _movementComponent;
        private NetworkRunner _networkRunner;

        private void Awake()
        {
            _networkRunner = DiContainerRef.Container.Resolve<NetworkRunner>();
        }

        private void Start()
        {
            _inputHandler = DiContainerRef.Container.Resolve<InputHandler>();
            _networkRunner.AddCallbacks(this);

           // _movementContext = _inputHandler.GetContext<MovementContext>();
        }

        public override void FixedUpdateNetwork()
        {
            _movementInput = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0f, UnityEngine.Input.GetAxis("Vertical"));
        }

        public NetworkInputData GetNetworkData()
        {
            var data = new NetworkInputData();
            data.movementInput = _movementInput;

            return data;
        }
        
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            _movementComponent = GetComponent<MovementComponent>();
            
            NetworkInputData inputData = new NetworkInputData
            {
                movementInput = _movementInput
            };

            input.Set(inputData);
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