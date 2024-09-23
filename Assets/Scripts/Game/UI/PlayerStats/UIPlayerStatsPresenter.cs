using Fusion;
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
            
            var localPlayer = await _playersService.GetLocalPlayer();
            
            Debug.Log("GetLocalPlayer");

            
            localPlayer.Model
                .SpeedMoving
                .AsObservable()
                .Subscribe(value => View.UpdateMoveSpeed(value))
                .AddTo(View);
            
            localPlayer.Model
                .DamagePerSecond
                .AsObservable()
                .Subscribe(value => View.UpdateDPS(value))
                .AddTo(View);
            
            localPlayer.Model
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