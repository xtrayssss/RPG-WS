using Assets.Interfaces;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Enemies.BaseEnemy
{
    public class EnemyAcceptedDamage : IDamagable
    {
        private EnemyData enemyData;
        private AniamationManager.EnemyAnimation enemyAnimation;
        public EnemyAcceptedDamage(EnemyData enemyData, AniamationManager.EnemyAnimation enemyAnimation)
        {
            this.enemyData = enemyData;
            this.enemyAnimation = enemyAnimation;
        }
        public void AcceptDamage(int damage)
        {
            enemyData.Health -= damage;
        }
    }
}
