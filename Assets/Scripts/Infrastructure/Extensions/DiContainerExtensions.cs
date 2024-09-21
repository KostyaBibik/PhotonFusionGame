using Infrastructure.UI;
using Zenject;

public static class DiContainerExtensions
{
    public static void BindPresenterWithView<TPresenter, TView>(this DiContainer container)
        where TPresenter : UIPresenter<TView>
        where TView : IUIView
    {
        container.BindInterfacesAndSelfTo<TView>()
            .FromComponentInHierarchy()
            .AsSingle();

        container.BindInterfacesAndSelfTo<TPresenter>()
            .AsSingle()
            .NonLazy();
    }
}