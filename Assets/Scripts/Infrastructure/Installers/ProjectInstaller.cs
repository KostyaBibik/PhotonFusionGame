using Fusion;
using Game.Core.Initialization;
using Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private NetworkRunner _networkRunnerPrefab;
        [SerializeField] private NetworkSceneManagerDefault _networkSceneManager;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();

            BindPhotonComponents();
        }
        
        private void BindPhotonComponents()
        {
            var networkRunner = Container.InstantiatePrefabForComponent<NetworkRunner>(_networkRunnerPrefab);
            Container.Bind<NetworkRunner>().FromInstance(networkRunner).AsSingle();

            var networkSceneManager = Container.InstantiatePrefabForComponent<NetworkSceneManagerDefault>(_networkSceneManager);
            Container.Bind<NetworkSceneManagerDefault>().FromInstance(networkSceneManager).AsSingle();

            Container.Bind<NetworkInitializer>().AsSingle().NonLazy();
        }
    }
}