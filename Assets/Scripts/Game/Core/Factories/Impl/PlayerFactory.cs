using DataBase.Configs;
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

        public PlayerFactory(DiContainer container) : base(container)
        {
        }
        
        public PlayerPresenter Create(Vector3 spawnPosition)
        {
            var speed = _playerPrefabConfig.MoveSpeed;
            var attackRange = _playerPrefabConfig.AttackRange;
            var damagePerSecond = _playerPrefabConfig.DamagePerSecond;

            var model = new PlayerModel(speed, attackRange, damagePerSecond);
            
            var view = Container.InstantiatePrefabForComponent<PlayerView>(_playerPrefabConfig.View, spawnPosition, Quaternion.identity, null);
            
            var movementComponent = Container.InstantiateComponent<MovementComponent>(view.gameObject);
            movementComponent.SetRigidbody(view.Rigidbody);
            
            model.SpeedMoving
                .AsObservable()
                .Subscribe(movementComponent.UpdateSpeedMoving)
                .AddTo(view);
            
            var presenter = new PlayerPresenter(view, model);
            
            return presenter;
        }
    }
}