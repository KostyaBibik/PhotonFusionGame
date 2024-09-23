using Fusion;

namespace Game.Core.Entities
{
    public abstract class NetworkEntityPresenter<TView, TModel> : EntityPresenter<TView, TModel>
    where TView : IEntityView
    where TModel : IEntityModel
    {
        public NetworkEntityPresenter(TView view, TModel model) : base(view, model)
        {
        }
    }
}