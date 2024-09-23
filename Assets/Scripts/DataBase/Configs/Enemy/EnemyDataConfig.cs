using System;
using System.Collections.Generic;
using Game.Core.Entities.EnemyImpl;
using UnityEngine;

namespace DataBase.Configs.Enemy
{
    [CreateAssetMenu(menuName = "Config/" + nameof(EnemyDataConfig),
        fileName = nameof(EnemyDataConfig))]
    public class EnemyDataConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyVO> _enemiesVos;

        public EnemyVO GetEnemyData(EEnemyGrade grade)
        {
            foreach (var enemyVo in _enemiesVos)
            {
                if (enemyVo.Grade == grade)
                {
                    return enemyVo;
                }
            }

            throw new Exception($"Not find EnemyVo with grade: {grade}");
        }
    }
}