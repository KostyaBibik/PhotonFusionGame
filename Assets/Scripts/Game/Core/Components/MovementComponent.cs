using Fusion;
using UnityEngine;

namespace Game.Core.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : NetworkBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _directionInput;
        
        [Networked] private float _speedMoving { get; set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public override void FixedUpdateNetwork()
        {
            if(!Object.InputAuthority)
                return;
            
            _directionInput = GetInput(out NetworkInputData data)
                ? data.movementInput 
                : Vector3.zero;
            
            Debug.Log("MovementComponent " + _directionInput);

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