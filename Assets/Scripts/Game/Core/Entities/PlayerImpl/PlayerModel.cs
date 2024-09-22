using UniRx;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerModel : IEntityModel
    {
        public readonly ReactiveProperty<float> SpeedMoving = new();
        public readonly ReactiveProperty<float> AttackRange = new();
        public readonly ReactiveProperty<float> DamagePerSecond = new();

        public PlayerModel(
            float speedMoving,
            float attackRange,
            float damagePerSecond
        )
        {
            SpeedMoving.Value = speedMoving;
            AttackRange.Value = attackRange;
            DamagePerSecond.Value = damagePerSecond;
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