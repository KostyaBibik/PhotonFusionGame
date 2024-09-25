using Fusion;
using UniRx;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerModel : NetworkBehaviour, IEntityModel
    {
        public readonly ReactiveProperty<float> SpeedMoving = new();
        public readonly ReactiveProperty<float> AttackRange = new();
        public readonly ReactiveProperty<float> DamagePerSecond = new();
        public readonly ReactiveProperty<int> CountKills = new();
    
        [Networked(OnChanged = nameof(OnSpeedMovingChanged))]
        public float NetworkedSpeedMoving { get; set; }

        [Networked(OnChanged = nameof(OnAttackRangeChanged))]
        public float NetworkedAttackRange { get; set; }

        [Networked(OnChanged = nameof(OnDamagePerSecondChanged))]
        public float NetworkedDamagePerSecond { get; set; }
        
        [Networked(OnChanged = nameof(OnCountKillsChanged))]
        public int NetworkedCountKills { get; set; }
        
        private PlayerRef _player;
        
        public PlayerRef Player => _player;
        
        public void Init(
            float speedMoving,
            float attackRange,
            float damagePerSecond,
            PlayerRef player
        )
        {
            NetworkedSpeedMoving = speedMoving;
            NetworkedAttackRange = attackRange;
            NetworkedDamagePerSecond = damagePerSecond;
            NetworkedCountKills = 0;
            _player = player;
        }
        
        #region Network Update Callbacks
        public static void OnSpeedMovingChanged(Changed<PlayerModel> changed)
        {
            changed.Behaviour.SpeedMoving.Value = changed.Behaviour.NetworkedSpeedMoving;
        }

        public static void OnAttackRangeChanged(Changed<PlayerModel> changed)
        {
            changed.Behaviour.AttackRange.Value = changed.Behaviour.NetworkedAttackRange;
        }

        public static void OnDamagePerSecondChanged(Changed<PlayerModel> changed)
        {
            changed.Behaviour.DamagePerSecond.Value = changed.Behaviour.NetworkedDamagePerSecond;
        }
        
        public static void OnCountKillsChanged(Changed<PlayerModel> changed)
        {
            changed.Behaviour.CountKills.Value = changed.Behaviour.NetworkedCountKills;
        }
        #endregion
    }
}