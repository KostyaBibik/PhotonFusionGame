using Game.Core.Entities.PlayerImpl;
using UnityEngine;

namespace DataBase.Configs
{
    [CreateAssetMenu(menuName = "Config/" + nameof(PlayerPrefabConfig),
        fileName = nameof(PlayerPrefabConfig))]
    public class PlayerPrefabConfig : ScriptableObject
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _damagePerSecond;

        public PlayerView View => _view;
        public float MoveSpeed => _moveSpeed;
        public float AttackRange => _attackRange;
        public float DamagePerSecond => _damagePerSecond;
    }
}