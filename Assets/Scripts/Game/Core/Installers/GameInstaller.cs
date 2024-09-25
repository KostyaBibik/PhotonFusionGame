using Game.Core.Factories.Impl;
using Game.Core.Services;
using Game.Core.Systems;
using Game.Core.Systems.Enemy;
using Game.Core.Systems.Player;
using UnityEngine;
using Zenject;

namespace Game.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneHandler _sceneHandler;
        
        public override void InstallBindings()
        {
            BindSceneComponents();
            
            BindServices();
            
            BindFactories();

            BindTrackSystems();
        }

        private void BindSceneComponents()
        {
            Container
                .Bind<SceneHandler>()
                .FromInstance(_sceneHandler)
                .AsTransient();
            
            Container
                .Bind<Camera>()
                .FromInstance(Camera.main)
                .AsTransient();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesAndSelfTo<PlayersService>()
                .AsSingle()
                .NonLazy(); 
            
            Container
                .BindInterfacesAndSelfTo<EnemiesService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFactories()
        {
            Container
                .Bind<PlayerFactory>()
                .AsTransient()
                .NonLazy();
            
            Container
                .Bind<EnemyFactory>()
                .AsTransient()
                .NonLazy();
        }

        private void BindTrackSystems()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerTrackerSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<EnemySpawnSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<AttackSystem>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<UpgradeHandler>()
                .AsSingle()
                .NonLazy(); 
            
            Container
                .BindInterfacesAndSelfTo<EnemyDeathSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}