using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputActionsInstaller : MonoInstaller
    {
        [SerializeField] private InputHandler _inputHandler;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>()
                .FromInstance(_inputHandler)
                .AsSingle();
        }
    }
}