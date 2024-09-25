using DataBase.Configs.Enemy;
using DataBase.Configs.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigsInstaller),
        menuName = "Installers/" + nameof(ConfigsInstaller))]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private PlayerDataConfig _playerDataConfig;
        [SerializeField] private PlayerUpgradeConfig _playerUpgradeConfig;
        [SerializeField] private EnemyDataConfig _enemyDataConfig;

        public override void InstallBindings()
        {
            BindPlayerConfigs();

            BindEnemyConfigs();
        }

        private void BindPlayerConfigs()
        {
            Container.BindInstance(_playerDataConfig);
            Container.BindInstance(_playerUpgradeConfig);
        }

        private void BindEnemyConfigs()
        {
            Container.BindInstance(_enemyDataConfig);
        }
    }
}