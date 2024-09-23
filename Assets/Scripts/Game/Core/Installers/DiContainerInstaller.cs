using System;
using Zenject;

namespace Game.Core.Installers
{
    public class DiContainerInstaller : MonoInstaller, IDisposable
    {
        [Inject] private DiContainer _diContainer;

        public override void InstallBindings()
        {
            DiContainerRef.Container = _diContainer;
        }

        public void Dispose()
        {
            DiContainerRef.Container.UnbindAll();
            DiContainerRef.Container = null;
        }
    }
}