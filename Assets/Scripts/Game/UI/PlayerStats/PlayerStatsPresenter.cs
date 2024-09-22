using Infrastructure.UI;
using Zenject;

namespace Game.UI.PlayerStats
{
    public class PlayerStatsPresenter : UIPresenter<PlayerStatsView>, ITickable
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


        /// <summary>
        /// TEST -> CLEAR
        /// </summary>
        public void Tick()
        {
           UnityEngine.Debug.Log($"_movementContext.Value: {_movementContext.Value}");
        }

        public override void Dispose()
        {
            
        }
    }
}