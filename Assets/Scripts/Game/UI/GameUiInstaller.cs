using Game.UI.PlayerStats;
using Zenject;

namespace Game.UI
{
    public class GameUiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerStats();
        }

        private void BindPlayerStats()
        {
            Container.BindPresenterWithView<PlayerStatsPresenter, PlayerStatsView>();
        }
    }
}