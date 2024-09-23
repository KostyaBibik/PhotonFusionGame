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
        
        public EnemyFactory(DiContainer container) : base(container)
        {
        }

        public void Create(EEnemyGrade grade)
        {
            if (!_networkRunner.IsRunning)
                return;

            var data = _enemyDataConfig.GetEnemyData(grade);
            var pos = _sceneHandler.GetFreeSpawnPoint();
            
            var view = _networkRunner.Spawn<EnemyView>(data.Prefab, position:pos);

        }
    }
}