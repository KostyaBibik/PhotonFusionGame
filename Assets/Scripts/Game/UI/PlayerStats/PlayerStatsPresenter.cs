using System.Linq;
using Fusion;
using Infrastructure.UI;
using Zenject;

namespace Game.UI.PlayerStats
{
    public class PlayerStatsPresenter : UIPresenter<PlayerStatsView>, ITickable
    {
        [Inject] private InputHandler _inputHandler;
        [Inject] private NetworkRunner _networkRunner;
        

        public PlayerStatsPresenter(PlayerStatsView view) : base(view)
        {
        }

        public override void Initialize()
        {
            
        }

        public override void Dispose()
        {
            
        }

        public void Tick()
        {
            var value = 0;
            foreach (var playerRef in _networkRunner.ActivePlayers)
            {
                value += playerRef.PlayerId;
            }
            
            View.UpdateMoveSpeed(value);
            
            var value2 = 0;
            foreach (var playerRef in _networkRunner.ActivePlayers)
            {
                value2 += playerRef.PlayerId;
            }
            
            View.UpdateRangeAttack(_networkRunner.ActivePlayers.Count());
        }
    }
}