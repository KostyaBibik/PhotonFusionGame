using Game.UI.PlayerStats;
using Game.UI.UpgradeButton;
using Zenject;

namespace Game.UI
{
    public class GameUiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerStats();
            BindUpgradeButton();
        }

        private void BindPlayerStats()
        {
            Container.BindPresenterWithView<UIPlayerStatsPresenter, PlayerStatsView>();
        }

        private void BindUpgradeButton()
        {
            Container.BindPresenterWithView<UpgradeButtonPresenter, UpgradeButtonView>();
        }
    }
}