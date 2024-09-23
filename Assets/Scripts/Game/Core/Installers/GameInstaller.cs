using Game.Core.Factories.Impl;
using Game.Core.Services;
using Game.Core.Systems;
using UnityEngine;
using Zenject;

namespace Game.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneHandler _sceneHandler;
        
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
            
            Container.Bind<SceneHandler>()
                .FromInstance(_sceneHandler)
                .AsTransient();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<EnemyFactory>()
                .AsTransient()
                .NonLazy();
        }

        private void BindTrackSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerTrackerSystem>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemySpawnSystem>()
                .AsSingle();
        }
    }
}