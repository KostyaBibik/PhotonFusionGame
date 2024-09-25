using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Game.Core.Components;
using Game.Core.Entities;
using Game.Core.Entities.PlayerImpl;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Systems.Player
{
    public class AttackSystem : ITickable, IInitializable, IDisposable
    {
        [Inject] private NetworkRunner _networkRunner;
        [Inject] private PlayersService _playersService;
        
        private AttackComponent _attackComponent;
        private int _maxTargets = 3;
        private float _damagePerSecond;
        private bool _isInitialized;
        private PlayerPresenter _localPlayer;

        private readonly CompositeDisposable _disposables = new();

        public async void Initialize()
        {
            _localPlayer = await _playersService.GetLocalPlayer();

            _attackComponent = _localPlayer.View.AttackComponent;
            
            _localPlayer.Model
                .DamagePerSecond
                .Subscribe(value => _damagePerSecond = value)
                .AddTo(_disposables);
            
            _isInitialized = true;
        }

        public void Tick()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            ApplyDamageToEnemies();
        }

        private void ApplyDamageToEnemies()
        {
            var enemiesOnRange = _attackComponent.EnemiesOnRange;
            if (enemiesOnRange.Count == 0)
            {
                return;
            }

            var targetEnemies = GetLiveTargets(enemiesOnRange.ToList());

            if (enemiesOnRange.Count > _maxTargets)
            {
                targetEnemies = GetNearestTargets(enemiesOnRange, _attackComponent.transform.position, _maxTargets);
            }
            
            ApplyDamage(targetEnemies, _damagePerSecond);
        }

        private List<IDamageable> GetNearestTargets(IReadOnlyCollection<IDamageable> enemies, Vector3 attackerPosition, int maxTargets)
        {
            var sortedEnemies = new List<IDamageable>(
                enemies
                    .Where(enemy => enemy != null && (Component)enemy != null)  
            );

            sortedEnemies.Sort((a, b) => 
                Vector3.Distance(attackerPosition, ((Component)a).transform.position)
                .CompareTo(Vector3.Distance(attackerPosition, ((Component)b).transform.position)));

            return sortedEnemies.GetRange(0, Mathf.Min(maxTargets, sortedEnemies.Count));
        }

        private List<IDamageable> GetLiveTargets(IReadOnlyCollection<IDamageable> enemies)
            => enemies
                .Where(enemy => enemy != null && (Component)enemy != null && enemy.CanBeAttacked()) 
                .ToList();

        private void ApplyDamage(List<IDamageable> targets, float damagePerSecond)
        {
            foreach (var target in targets)
            {
                if (target == null || (Component)target == null)  
                {
                    continue;
                }
                
                target.TakeDamage(damagePerSecond * Time.deltaTime, _localPlayer.Object.InputAuthority);
            }
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}
