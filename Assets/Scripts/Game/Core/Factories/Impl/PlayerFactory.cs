﻿using System;
using DataBase.Configs;
using DataBase.Configs.Player;
using Fusion;
using Game.Core.Components;
using Game.Core.Entities.PlayerImpl;
using Game.Core.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Core.Factories.Impl
{
    public sealed class PlayerFactory : EntityFactory<PlayerPresenter, PlayerView, PlayerModel>
    {
        [Inject] private PlayerDataConfig _playerDataConfig;
        [Inject] private readonly NetworkRunner _networkRunner;
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly InputHandler _inputHandler;
        [Inject] private readonly PlayersService _playersService;
        [Inject] private readonly SceneHandler _sceneHandler;
        
        public PlayerFactory(DiContainer container) : base(container)
        {
        }
        
        public PlayerPresenter Create(PlayerRef player)
        {
            if (!_networkRunner.IsRunning)
                return null;
            
            var speed = _playerDataConfig.MoveSpeed;
            var attackRange = _playerDataConfig.AttackRange;
            var damagePerSecond = _playerDataConfig.DamagePerSecond;
            
            var pos = _sceneHandler.GetFreeSpawnPoint(); 
           
            var view = _networkRunner.Spawn<PlayerView>(_playerDataConfig.View, inputAuthority:player, position:pos);

            var movementComponent = view.MovementComponent;

            var model = view.PlayerModel;
            model.Init(speed, attackRange, damagePerSecond, player);
            
            model.SpeedMoving
                .AsObservable()
                .Subscribe(movementComponent.UpdateSpeedMoving)
                .AddTo(view);
            
            var presenter = new PlayerPresenter(view, model);
            _playersService.AddPlayer(presenter);
            
            _networkRunner.SetPlayerObject(player, view.Object);
            
            return presenter;
        }
    }
}