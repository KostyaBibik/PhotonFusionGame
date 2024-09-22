namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerPresenter : IEntityPresenter<PlayerView, PlayerModel>
    {
        public PlayerPresenter(PlayerView view, PlayerModel model) : base(view, model)
        {
        }
    }
}