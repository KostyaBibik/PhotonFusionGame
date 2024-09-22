using DataBase.Configs;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigsInstaller),
        menuName = "Installers/" + nameof(ConfigsInstaller))]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private PlayerPrefabConfig _playerPrefabConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerPrefabConfig);
        }
    }
}