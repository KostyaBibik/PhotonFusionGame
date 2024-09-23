using Game.Core.Factories.Impl;
using Game.Core.Services;
using Game.Core.Systems;
using Zenject;

namespace Game.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            
            BindFactories();

            BindTrackSystems();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<PlayersService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>()
                .AsTransient()
                .NonLazy();
        }

        private void BindTrackSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerTrackerSystem>()
                .AsSingle();
        }
    }
}