namespace Game.Core.Entities.EnemyImpl
{
    public class EnemyPresenter : NetworkEntityPresenter<EnemyView, EnemyModel>
    {
        public EnemyPresenter(EnemyView view, EnemyModel model) : base(view, model)
        {
        }
    }
}