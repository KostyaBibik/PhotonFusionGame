using Infrastructure.UI;
using UniRx;

namespace Game.UI.UpgradeButton
{
    public class UpgradeButtonPresenter : UIPresenter<UpgradeButtonView>
    {
        public UpgradeButtonPresenter(UpgradeButtonView view) : base(view)
        {
        }

        public override void Initialize()
        {
            View.OnUpgradeClicked
                .AsObservable()
                .Subscribe(_ => UnityEngine.Debug.Log("View.OnUpgradeClicked"))
                .AddTo(View);
        }
    }
}