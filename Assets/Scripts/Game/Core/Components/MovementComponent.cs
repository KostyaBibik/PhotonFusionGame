using System;
using Fusion;
using Game.Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : NetworkBehaviour
    {
        private InputHandler _inputHandler; 
        private NetworkRunner _networkRunner;
        
        private AxisInputContext _movementContext;
        private Rigidbody _rigidbody;
        private NetworkRigidbody _networkRigidbody;
        private Vector3 _directionInput;
        
        [Networked] private float _speedMoving { get; set; }
        [Networked] private Vector3 _directionMove { get; set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            if (!Object.HasInputAuthority)
            {
                return;
            }
            
            _inputHandler = DiContainerRef.Container.Resolve<InputHandler>();
            _networkRunner = DiContainerRef.Container.Resolve<NetworkRunner>();

            _movementContext = _inputHandler.GetContext<MovementContext>();
        }
        
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                Debug.Log($"(GetInput( {data.movementInput}");

                _directionInput = data.movementInput;
            }
            else
            {
                _directionInput = Vector3.zero;
            }

            _rigidbody.velocity = _directionInput * _speedMoving;

            /*if (!Object.HasInputAuthority || _movementContext == null)
            {
                return;
            }

            var input = _movementContext.Value;

            var movement = new Vector3(input.x, 0, input.y) * (_speedMoving * Time.fixedDeltaTime);*/

        }
        
        
        public void UpdateSpeedMoving(float value)
            => _speedMoving = value;
    }
    
    
}