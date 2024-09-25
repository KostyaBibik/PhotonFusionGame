using Game.Core.Entities;
using Zenject;

namespace Game.Core.Factories
{
    public abstract class EntityFactory<TPresenter, TView, TModel> 
        where TPresenter : IEntityPresenter<TView, TModel>
        where TView : IEntityView
        where TModel : IEntityModel
    {
        protected DiContainer Container;

        public EntityFactory(DiContainer container)
        {
            Container = container;
        }
    }
}