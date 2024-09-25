using System;
using Fusion;
using Game.Core.Entities.EnemyImpl;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Systems.Enemy
{
    public class EnemyDeathSystem: IInitializable, IDisposable
    {
        [Inject] private readonly EnemySpawnSystem _enemySpawnSystem;
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly EnemiesService _enemiesService;
        [Inject] private readonly PlayersService _playersService;

        private CompositeDisposable _compositeDisposable = new();
        
        public void Initialize()
        {
            var enemies = _enemiesService.Enemies;
            foreach (var enemy in enemies)
            {
                enemy.OnDeath += HandleEnemyDeath;
            }

            _enemiesService
                .OnEnemyAdded
                .Subscribe(newEnemy => newEnemy.OnDeath += HandleEnemyDeath)
                .AddTo(_compositeDisposable);
        }

        private void HandleEnemyDeath(EnemyPresenter enemy)
        {
            if (!_networkRunner.IsServer)
            {
                return;
            }

            _enemiesService.RemoveEnemy(enemy);
            
            _networkRunner.Despawn(enemy.Object);

            var playerKiller = _playersService.GetPlayer(enemy.Model.LastDamageInvoker);
            if(playerKiller != null)
            {
                Debug.Log("AddCountKills");
                playerKiller.AddCountKills();
            }
            
            RespawnEnemy(enemy.Model.Grade);
        }

        private void RespawnEnemy(EEnemyGrade grade)
        {
            _enemySpawnSystem.SpawnEnemy(grade);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}