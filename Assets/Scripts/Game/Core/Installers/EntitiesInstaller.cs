using Game.Core.Entities.PlayerImpl;
using Game.Core.Factories;
using Game.Core.Factories.Impl;
using Zenject;

namespace Game.Core.Installers
{
    public class EntitiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<EntityFactory<PlayerPresenter, PlayerView, PlayerModel>>()
                .To<PlayerFactory>()
                .AsTransient();
        }
    }
}