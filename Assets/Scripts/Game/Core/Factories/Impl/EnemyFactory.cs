using DataBase.Configs.Enemy;
using Fusion;
using Game.Core.Entities.EnemyImpl;
using Game.Core.Services;
using Zenject;

namespace Game.Core.Factories.Impl
{
    public class EnemyFactory : EntityFactory<EnemyPresenter, EnemyView, EnemyModel>
    {
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly EnemyDataConfig _enemyDataConfig;
        [Inject] private readonly SceneHandler _sceneHandler;
        [Inject] private readonly EnemiesService _enemiesService;
        
        public EnemyFactory(DiContainer container) : base(container)
        {
        }

        public void Create(EEnemyGrade grade)
        {
            if (!_networkRunner.IsRunning)
                return;

            var data = _enemyDataConfig.GetEnemyData(grade);
            var pos = _sceneHandler.GetFreeSpawnPoint();
            
            var presenter = _networkRunner.Spawn(data.Prefab, position:pos);
         
            presenter.Model.Init(data.Grade, data.Health);
            
            _enemiesService.AddEnemy(presenter);
        }
    }
}