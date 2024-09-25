using System;
using System.Collections.Generic;
using Game.Core.Entities.EnemyImpl;
using UniRx;

namespace Game.Core.Services
{
    public class EnemiesService
    {
        private List<EnemyPresenter> _enemies = new List<EnemyPresenter>();
        private Subject<EnemyPresenter> _onEnemyAdded = new Subject<EnemyPresenter>();

        public IObservable<EnemyPresenter> OnEnemyAdded => _onEnemyAdded;

        public IReadOnlyCollection<EnemyPresenter> Enemies => _enemies;

        public void AddEnemy(EnemyPresenter newEnemy)
        {
            if (!_enemies.Contains(newEnemy))
            {
                _enemies.Add(newEnemy);
                _onEnemyAdded.OnNext(newEnemy);
            }
        }

        public void RemoveEnemy(EnemyPresenter enemy)
        {
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
}