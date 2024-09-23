using Fusion;
using UniRx;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerModel : IEntityModel
    {
        public readonly ReactiveProperty<float> SpeedMoving = new();
        public readonly ReactiveProperty<float> AttackRange = new();
        public readonly ReactiveProperty<float> DamagePerSecond = new();
        
        private readonly PlayerRef _player;
        
        public PlayerRef Player => _player;
        
        public PlayerModel(
            float speedMoving,
            float attackRange,
            float damagePerSecond,
            PlayerRef player
        )
        {
            SpeedMoving.Value = speedMoving;
            AttackRange.Value = attackRange;
            DamagePerSecond.Value = damagePerSecond;
            _player = player;
        }
        
        public void UpdateSpeedMoving(float value)
        {
            SpeedMoving.Value = value;
        }
        
        public void UpdateAttackRange(float value)
        {
            AttackRange.Value = value;
        }
        
        public void UpdateDamagePerSecond(float value)
        {
            DamagePerSecond.Value = value;
        }
    }
}