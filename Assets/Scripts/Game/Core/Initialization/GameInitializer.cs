using Game.Core.Factories.Impl;
using UnityEngine;
using Zenject;

namespace Game.Core.Initialization
{
    public class GameInitializer : IInitializable
    {
        [Inject] private PlayerFactory _playerFactory;
        
        public void Initialize()
        {
            _playerFactory.Create(new Vector3(0, 0, 0)); 
        }
    }
}