using Fusion;

namespace Game.Core.Entities
{
    public abstract class NetworkEntityPresenter<TView, TModel> : NetworkBehaviour, IEntityPresenter<TView, TModel>
    where TView : IEntityView
    where TModel : IEntityModel
    {
        public TView View { get; }
        public TModel Model { get; }
    }
}