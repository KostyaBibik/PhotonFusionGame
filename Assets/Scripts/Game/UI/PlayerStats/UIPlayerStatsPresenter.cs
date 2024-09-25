using Fusion;
using Game.Core.Services;
using Infrastructure.UI;
using UniRx;
using Zenject;

namespace Game.UI.PlayerStats
{
    public class UIPlayerStatsPresenter : UIPresenter<PlayerStatsView>
    {
        [Inject] private NetworkRunner _networkRunner;
        [Inject] private PlayersService _playersService;

        private CompositeDisposable _compositeDisposable = new();
        
        public UIPlayerStatsPresenter(PlayerStatsView view) : base(view)
        {
        }

        public override async void Initialize()
        {
            var playerObject = await _playersService.GetLocalPlayer();
            var model = playerObject.Model;
            
            model
                .SpeedMoving
                .AsObservable()
                .Subscribe(value => View.UpdateMoveSpeed(value))
                .AddTo(_compositeDisposable);
            
            model
                .DamagePerSecond
                .AsObservable()
                .Subscribe(value => View.UpdateDPS(value))
                .AddTo(_compositeDisposable);
            
            model
                .AttackRange
                .AsObservable()
                .Subscribe(value => View.UpdateRangeAttack(value))
                .AddTo(_compositeDisposable);
            
            model
                .CountKills
                .AsObservable()
                .Subscribe(value => View.UpdateCountKills(value))
                .AddTo(_compositeDisposable);
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}