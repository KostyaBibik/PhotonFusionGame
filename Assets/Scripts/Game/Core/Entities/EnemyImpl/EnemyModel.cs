using Fusion;
using UniRx;

namespace Game.Core.Entities.EnemyImpl
{
    public class EnemyModel : NetworkBehaviour, IEntityModel
    {
        public readonly ReactiveProperty<float> Health = new();
        
        [Networked(OnChanged = nameof(OnHealthChanged))]
        public float NetworkedHealth { get; set; }

        [Networked] public float StartHealth { get; set; }
        
        private EEnemyGrade _grade;
        public PlayerRef LastDamageInvoker { get; set; }
        
        public EEnemyGrade Grade => _grade;
        
        public void Init(
            EEnemyGrade grade,
            float health
        )
        {
            _grade = grade;
            NetworkedHealth = health;
            StartHealth = health;
        }
        
        public static void OnHealthChanged(Changed<EnemyModel> changed)
        {
            changed.Behaviour.Health.Value = changed.Behaviour.NetworkedHealth;
        }
    }
}