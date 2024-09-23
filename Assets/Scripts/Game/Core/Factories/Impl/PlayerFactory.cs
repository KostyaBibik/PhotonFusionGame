using System;
using DataBase.Configs;
using Fusion;
using Game.Core.Components;
using Game.Core.Entities.PlayerImpl;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Factories.Impl
{
    public sealed class PlayerFactory : EntityFactory<PlayerPresenter, PlayerView, PlayerModel>
    {
        [Inject] private PlayerPrefabConfig _playerPrefabConfig;
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly InputHandler _inputHandler;
        
        public PlayerFactory(DiContainer container) : base(container)
        {
        }
        
        public PlayerPresenter Create(Vector3 spawnPosition, PlayerRef player)
        {
            if (!_networkRunner.IsRunning)
                return null;
            
            var speed = _playerPrefabConfig.MoveSpeed;
            var attackRange = _playerPrefabConfig.AttackRange;
            var damagePerSecond = _playerPrefabConfig.DamagePerSecond;

            var model = new PlayerModel(speed, attackRange, damagePerSecond, player.PlayerId);

            var view = _networkRunner.Spawn<PlayerView>(_playerPrefabConfig.View, inputAuthority:player);

            var movementComponent = view.MovementComponent;
            
            //movementComponent.SetupOnClients(_inputHandler, _networkRunner);
           // var movementComponent = Container.InstantiateComponent<MovementComponent>(view.gameObject);
           
            model.SpeedMoving
                .AsObservable()
                .Subscribe(movementComponent.UpdateSpeedMoving)
                .AddTo(view);
            
            var presenter = new PlayerPresenter(view, model);
            
            return presenter;
        }
    }
}