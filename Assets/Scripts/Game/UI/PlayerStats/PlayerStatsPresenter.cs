using Infrastructure.UI;
using Zenject;

namespace Game.UI.PlayerStats
{
    public class PlayerStatsPresenter : UIPresenter<PlayerStatsView>
    {
        [Inject] private InputHandler _inputHandler;
        private AxisInputContext _movementContext;

        public PlayerStatsPresenter(PlayerStatsView view) : base(view)
        {
        }

        public override void Initialize()
        {
            _movementContext = _inputHandler.GetContext<MovementContext>();
        }


        public override void Dispose()
        {
            
        }
    }
}