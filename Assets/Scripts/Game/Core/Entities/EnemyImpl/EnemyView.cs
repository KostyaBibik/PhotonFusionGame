using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Entities.EnemyImpl
{
    public class EnemyView : NetworkBehaviour, IEntityView
    {
        [SerializeField] private Slider _healthSider;
        [SerializeField] private TextMeshProUGUI _text;

        public void InvokeUpdateSliderValueLocal(float value)
        {
            UpdateSliderValue(value);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void InvokeUpdateSliderValueRPC(float value)
        {
            UpdateSliderValue(value);
        }

        private void UpdateSliderValue(float value)
        {
            value = Mathf.Clamp(value, 0, 1);
            _text.text = value.ToString();
            _healthSider.value = value;
        }
    }
}