using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class MovementComponent : MonoBehaviour
    {
        [Inject] private InputHandler _inputHandler;
        
        private AxisInputContext _movementContext;
        private float _speedMoving;
        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _movementContext = _inputHandler.GetContext<MovementContext>();
        }

        private void FixedUpdate()
        {
            var input = _movementContext.Value;

            var movement = new Vector3(input.x, 0, input.y) * (_speedMoving * Time.fixedDeltaTime);

            _rigidbody.MovePosition(transform.position + movement);
        }

        public void SetRigidbody(Rigidbody rigidbody)
            => _rigidbody = rigidbody;

        public void UpdateSpeedMoving(float value)
        {
            _speedMoving = value;
        }
    }
}