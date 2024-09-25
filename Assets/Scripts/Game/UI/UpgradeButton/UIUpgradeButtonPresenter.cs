using System;
using Infrastructure.UI;
using UniRx;

namespace Game.UI.UpgradeButton
{
    public class UIUpgradeButtonPresenter : UIPresenter<UpgradeButtonView>
    {
        public IObservable<Unit> OnUpgradeClicked => View.OnUpgradeClicked;
        
        public UIUpgradeButtonPresenter(UpgradeButtonView view) : base(view)
        {
        }
    }
}