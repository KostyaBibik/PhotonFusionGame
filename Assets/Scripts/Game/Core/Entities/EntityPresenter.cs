using Fusion;

namespace Game.Core.Entities
{
    public abstract class EntityPresenter<TView, TModel> 
        where TView : IEntityView
        where TModel : IEntityModel
    {
        private TView _view;
        
        private TModel _model;
        
        public TView View => _view;
        public TModel Model => _model;

        public EntityPresenter(TView view, TModel model)
        {
            _view = view;
            _model = model;
        }
    }
}