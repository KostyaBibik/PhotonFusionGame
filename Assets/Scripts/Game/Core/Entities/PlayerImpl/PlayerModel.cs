using UniRx;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerModel : IEntityModel
    {
        public readonly ReactiveProperty<float> SpeedMoving = new();
        public readonly ReactiveProperty<float> AttackRange = new();
        public readonly ReactiveProperty<float> DamagePerSecond = new();
        private readonly int _playerId;

        public int PlayerId => _playerId;
        
        public PlayerModel(
            float speedMoving,
            float attackRange,
            float damagePerSecond,
            int playerId
        )
        {
            SpeedMoving.Value = speedMoving;
            AttackRange.Value = attackRange;
            DamagePerSecond.Value = damagePerSecond;
            _playerId = playerId;
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