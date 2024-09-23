using DataBase.Configs;
using DataBase.Configs.Enemy;
using DataBase.Configs.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigsInstaller),
        menuName = "Installers/" + nameof(ConfigsInstaller))]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private PlayerDataConfig _playerDataConfig;
        [SerializeField] private EnemyDataConfig _enemyDataConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerDataConfig);
            Container.BindInstance(_enemyDataConfig);
        }
    }
}