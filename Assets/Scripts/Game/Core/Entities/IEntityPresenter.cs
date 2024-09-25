
namespace Game.Core.Entities
{
    public interface IEntityPresenter<TView, TModel> 
        where TView : IEntityView
        where TModel : IEntityModel
    {
        public TView View { get; }
        public TModel Model { get; }
    }
}