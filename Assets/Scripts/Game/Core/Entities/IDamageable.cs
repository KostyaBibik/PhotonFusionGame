using System;
using Fusion;

namespace Game.Core.Entities
{
    public interface IDamageable<T> : IDamageable
    {
        Action<T> OnDeath { get; set; }
    }
    
    public interface IDamageable
    {
        bool CanBeAttacked();
        void TakeDamage(float damage, PlayerRef damageInvoker);
    }
}