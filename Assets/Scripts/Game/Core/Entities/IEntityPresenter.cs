namespace Game.Core.Entities
{
    public abstract class IEntityPresenter<TView, TModel>
        where TView : IEntityView
        where TModel : IEntityModel
    {
        private TView _view;
        
        private TModel _model;
        
        public TView View => _view;
        public TModel Model => _model;

        public IEntityPresenter(TView view, TModel model)
        {
            _view = view;
            _model = model;
        }
    }
}