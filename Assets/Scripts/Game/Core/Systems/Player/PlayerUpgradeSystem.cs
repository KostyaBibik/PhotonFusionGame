using DataBase.Configs.Player;
using Game.Core.Entities.PlayerImpl;

namespace Game.Core.Systems.Player
{
    public class PlayerUpgradeSystem
    {
        private readonly PlayerPresenter _playerPresenter;
        private readonly PlayerUpgradeConfig _upgradeConfig;

        public PlayerUpgradeSystem(
            PlayerPresenter playerPresenter,
            PlayerUpgradeConfig upgradeConfig
        )
        {
            _playerPresenter = playerPresenter;
            _upgradeConfig = upgradeConfig;
        }

        public void UpgradeRandomStat()
        {
            var totalWeight = _upgradeConfig.TotalSumChances;
            var randomValue = UnityEngine.Random.Range(0, totalWeight);
            
            if (randomValue < _upgradeConfig.SpeedUpgradeChance)
            {
                UpgradeSpeedMove();
            }
            else if (randomValue < _upgradeConfig.SpeedUpgradeChance + _upgradeConfig.RangeUpgradeChance)
            {
                UpgradeAttackRange();
            }
            else
            {
                UpgradeDamagePerSecond();
            }
        }

        private void UpgradeSpeedMove()
        {
            _playerPresenter.UpgradeSpeedMove(_upgradeConfig.SpeedUpgradeValue);
        }

        private void UpgradeAttackRange()
        {
            _playerPresenter.UpgradeRange(_upgradeConfig.RangeUpgradeValue);
        }

        private void UpgradeDamagePerSecond()
        {
            _playerPresenter.UpgradeDamagePerSecond(_upgradeConfig.DamageUpgradeValue);
        }
    }
}