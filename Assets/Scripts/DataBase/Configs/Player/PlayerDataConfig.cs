using Game.Core.Entities.PlayerImpl;
using UnityEngine;

namespace DataBase.Configs.Player
{
    [CreateAssetMenu(menuName = "Config/" + nameof(PlayerDataConfig),
        fileName = nameof(PlayerDataConfig))]
    public class PlayerDataConfig : ScriptableObject
    {
        [SerializeField] private PlayerPresenter _presenter;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _damagePerSecond;

        public PlayerPresenter Presenter => _presenter;
        public float MoveSpeed => _moveSpeed;
        public float AttackRange => _attackRange;
        public float DamagePerSecond => _damagePerSecond;
    }
}