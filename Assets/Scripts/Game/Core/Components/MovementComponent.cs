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
        
        [Networked] private float _speedMoving { get; set; }

        private void Start()
        {
            _networkRigidbody = GetComponent<NetworkRigidbody>();
            if (Object.HasInputAuthority)
            {
                _networkRigidbody.Rigidbody.isKinematic = false;
            }
            else
            {
                _networkRigidbody.Rigidbody.isKinematic = true;
            }
            
            if (!Object.HasInputAuthority)
            {
                return;
            }

            Debug.Log("Start  Object.HasInputAuthority");
            
            _inputHandler = DiContainerRef.Container.Resolve<InputHandler>();
            _networkRunner = DiContainerRef.Container.Resolve<NetworkRunner>();
            _rigidbody = GetComponent<Rigidbody>();

            _movementContext = _inputHandler.GetContext<MovementContext>();

           
        }
        
        public override void FixedUpdateNetwork()
        {
            if (!Object.HasInputAuthority || _movementContext == null)
            {
                return;
            }
            Debug.Log("Object.HasInputAuthority" + transform.name + Object.Id);

            var input = _movementContext.Value;
            Debug.Log($"{input.x} : {input.y} \\ {_speedMoving}");

            var movement = new Vector3(input.x, 0, input.y) * (_speedMoving * Time.fixedDeltaTime);

            transform.Translate(movement);
            //_rigidbody.MovePosition(transform.position + movement);
        }

        public void UpdateSpeedMoving(float value)
            => _speedMoving = value;
    }
}