using System;
using Game.Core.Entities.EnemyImpl;

namespace DataBase.Configs.Enemy
{
    [Serializable]
    public struct EnemyVO
    {
        public EEnemyGrade Grade;
        public EnemyView Prefab;
    }
}