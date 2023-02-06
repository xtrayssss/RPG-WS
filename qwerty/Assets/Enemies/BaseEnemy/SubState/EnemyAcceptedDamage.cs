using Assets.Interfaces;
using UnityEngine;

namespace Assets.Enemies.BaseEnemy
{
    public class EnemyAcceptedDamage : IDamagable
    {
        private EnemyData enemyData;
        public EnemyAcceptedDamage(EnemyData enemyData)
        {
            this.enemyData = enemyData;
        }
        public void AcceptDamage(int damage)
        {
            enemyData.Health -= damage;
        }
    }
}
