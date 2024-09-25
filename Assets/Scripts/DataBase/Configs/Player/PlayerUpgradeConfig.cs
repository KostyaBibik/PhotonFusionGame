using UnityEngine;

namespace DataBase.Configs.Player
{
    [CreateAssetMenu(menuName = "Config/" + nameof(PlayerUpgradeConfig),
        fileName = nameof(PlayerUpgradeConfig))]
    public class PlayerUpgradeConfig : ScriptableObject
    {
        [Header("Speed Upgrade")]
        [SerializeField] private float _speedUpgradeChance; 
        [SerializeField] private float _speedUpgradeValue; 

        [Header("Range Upgrade")]
        [SerializeField] private float _rangeUpgradeChance; 
        [SerializeField] private float _rangeUpgradeValue;  

        [Header("Damage Upgrade")]
        [SerializeField] private float _damageUpgradeChance; 
        [SerializeField] private float _damageUpgradeValue;

        public float TotalSumChances
            => _speedUpgradeChance
               + _rangeUpgradeChance 
               + _damageUpgradeChance;
        
        public float SpeedUpgradeChance => _speedUpgradeChance;
        public float SpeedUpgradeValue => _speedUpgradeValue;
        public float RangeUpgradeChance => _rangeUpgradeChance;
        public float RangeUpgradeValue => _rangeUpgradeValue;
        public float DamageUpgradeChance => _damageUpgradeChance;
        public float DamageUpgradeValue => _damageUpgradeValue;
    }
}