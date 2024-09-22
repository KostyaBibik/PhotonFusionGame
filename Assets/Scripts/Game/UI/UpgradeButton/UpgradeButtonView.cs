using System;
using Infrastructure.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UpgradeButton
{
    public class UpgradeButtonView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button _upgradeButton;
        
        public IObservable<Unit> OnUpgradeClicked => _upgradeButton.OnClickAsObservable();
    }
}