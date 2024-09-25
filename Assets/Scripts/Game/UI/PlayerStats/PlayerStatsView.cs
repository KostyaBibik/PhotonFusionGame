using System.Globalization;
using Infrastructure.UI;
using TMPro;
using UnityEngine;

namespace Game.UI.PlayerStats
{
    public class PlayerStatsView : MonoBehaviour, IUIView
    {
        [SerializeField] private TextMeshProUGUI _moveSpeed;
        [SerializeField] private TextMeshProUGUI _dps;
        [SerializeField] private TextMeshProUGUI _rangeAttack;
        [SerializeField] private TextMeshProUGUI _countKills;
        
        public void UpdateMoveSpeed(float value)
            => _moveSpeed.text = value.ToString(CultureInfo.InvariantCulture);
        
        public void UpdateDPS(float value)
            => _dps.text = value.ToString(CultureInfo.InvariantCulture);
        
        public void UpdateRangeAttack(float value)
            => _rangeAttack.text = value.ToString(CultureInfo.InvariantCulture);
        
        public void UpdateCountKills(int value)
            => _countKills.text = value.ToString(CultureInfo.InvariantCulture);
    }
}