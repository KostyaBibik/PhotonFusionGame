using Game.Core.Factories.Impl;
using Game.Core.Initialization;
using Zenject;

namespace Game.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            
            BindGameInitializer();
        }
        
        private void BindFactories()
        {
            Container.Bind<PlayerFactory>()
                .AsTransient()
                .NonLazy();
        }

        private void BindGameInitializer()
        {
            Container.BindInterfacesAndSelfTo<GameInitializer>()
                .AsSingle();
        }
    }
}