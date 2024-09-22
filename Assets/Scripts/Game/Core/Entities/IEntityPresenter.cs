namespace Game.Core.Entities
{
    public abstract class IEntityPresenter<TView, TModel>
        where TView : IEntityView
        where TModel : IEntityModel
    {
        private TView _view;
        
        protected TModel Model;
        
        public TView View => _view;

        public IEntityPresenter(TView view, TModel model)
        {
            _view = view;
            Model = model;
        }
    }
}