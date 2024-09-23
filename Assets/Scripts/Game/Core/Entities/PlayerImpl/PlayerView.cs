using System;
using Fusion;
using Game.Core.Components;
using UnityEngine;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerView : NetworkBehaviour, IEntityView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MovementComponent _movementComponent;
        
        public Rigidbody Rigidbody => _rigidbody;
        public MovementComponent MovementComponent => _movementComponent;
    }
}