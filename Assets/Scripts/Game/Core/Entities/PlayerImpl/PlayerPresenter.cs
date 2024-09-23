namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerPresenter : EntityPresenter<PlayerView, PlayerModel>
    {
        public PlayerPresenter(PlayerView view, PlayerModel model) : base(view, model)
        {
        }
    }
}