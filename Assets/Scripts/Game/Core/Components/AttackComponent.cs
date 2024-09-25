using System.Collections.Generic;
using Fusion;
using Game.Core.Entities;
using Game.Core.Entities.EnemyImpl;
using UnityEngine;

namespace Game.Core.Components
{
    [RequireComponent(typeof(Collider))]
    public sealed class AttackComponent : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;

        private List<IDamageable> _enemiesOnRange = new();
        
        public IReadOnlyList<IDamageable> EnemiesOnRange => _enemiesOnRange;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<IDamageable>() is IDamageable<EnemyPresenter> damageable)
            {
                if (!_enemiesOnRange.Contains(damageable))
                {
                    Debug.Log("damageable.OnDeath += TryRemoveEnemy;");
                    
                    damageable.OnDeath += TryRemoveEnemy;
                    _enemiesOnRange.Add(damageable);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IDamageable>() is IDamageable damageable)
            {
                Debug.Log("OnTriggerExit other");

                
                TryRemoveEnemy(damageable);
            }
        }

        private void TryRemoveEnemy(IDamageable enemy)
        {
            if (_enemiesOnRange.Contains(enemy))
            {
                Debug.Log("TryRemoveEnemy");
                
                _enemiesOnRange.Remove(enemy);
            }
        }

        public void UpdateRange(float range)
        {
            transform.localScale = new Vector3(range, range, range);
        }
    }
}