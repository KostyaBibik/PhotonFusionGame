using Fusion;
using Infrastructure.Boot;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private NetworkRunner _networkRunnerPrefab;
        [SerializeField] private NetworkSceneManagerDefault _networkSceneManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>()
                .AsSingle();
            Container.Bind<BootstrapView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}