using System;
using DataBase.Configs.Player;
using Game.Core.Services;
using Game.UI.UpgradeButton;
using UniRx;
using Zenject;

namespace Game.Core.Systems.Player
{
    public class UpgradeHandler : IInitializable, IDisposable
    {
        [Inject] private PlayersService _playersService;
        [Inject] private UIUpgradeButtonPresenter _uiUpgradeButtonPresenter;
        [Inject] private PlayerUpgradeConfig _playerUpgradeConfig;

        private CompositeDisposable _compositeDisposable = new();
        
        public async void Initialize()
        {
            var player = await _playersService.GetLocalPlayer();
            var upgradeSystem = new PlayerUpgradeSystem(player, _playerUpgradeConfig);

            _uiUpgradeButtonPresenter
                .OnUpgradeClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    upgradeSystem.UpgradeRandomStat();
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}