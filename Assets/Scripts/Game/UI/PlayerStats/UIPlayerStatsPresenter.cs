using System;
using System.Threading.Tasks;
using Fusion;
using Game.Core.Entities.PlayerImpl;
using Game.Core.Services;
using Infrastructure.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI.PlayerStats
{
    public class UIPlayerStatsPresenter : UIPresenter<PlayerStatsView>
    {
        [Inject] private NetworkRunner _networkRunner;
        [Inject] private PlayersService _playersService;

        public UIPlayerStatsPresenter(PlayerStatsView view) : base(view)
        {
        }

        public override async void Initialize()
        {
            Debug.Log("Initialize");

            await Task.Delay(2000);
            
            var playerObject = _networkRunner.GetPlayerObject(_networkRunner.LocalPlayer);

// Получаем компонент PlayerModel из объекта игрока
            var model = playerObject.GetComponent<PlayerView>().PlayerModel;

// Теперь вы можете использовать model для доступа к данным игрока
            if (model != null)
            {
                // Пример использования
                Debug.Log($"Скорость игрока: {model.SpeedMoving}");
            }
            else
            {
                Debug.LogError("PlayerModel не найден на объекте игрока.");
            }
            
            //var localPlayer = await _playersService.GetLocalPlayer();
            
            Debug.Log("GetLocalPlayer");

            
            model
                .SpeedMoving
                .AsObservable()
                .Subscribe(value => View.UpdateMoveSpeed(value))
                .AddTo(View);
            
            model
                .DamagePerSecond
                .AsObservable()
                .Subscribe(value => View.UpdateDPS(value))
                .AddTo(View);
            
            model
                .AttackRange
                .AsObservable()
                .Subscribe(value => View.UpdateRangeAttack(value))
                .AddTo(View);
        }

        public override void Dispose()
        {
            
        }
    }
}