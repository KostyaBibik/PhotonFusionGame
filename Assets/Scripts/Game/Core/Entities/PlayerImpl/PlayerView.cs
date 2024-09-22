using UnityEngine;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerView : MonoBehaviour, IEntityView
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
    }
}