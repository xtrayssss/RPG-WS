using UnityEngine;
namespace Assets.Enemies.BaseEnemy
{
    public class ZoneCheckerEnemy
    {
        private EnemyData enemyData;
        public ZoneCheckerEnemy(EnemyData enemyData)
        {
            this.enemyData = enemyData;
        }

        #region CheckEnterPlayerInAgroZone
        public bool CheckEnterPlayerInAgroZone(bool isEnter)
        {
            return enemyData.IsEnterAgroZone = isEnter;
        }
        #endregion

        #region CheckEnterPlayerInAttackZone
        public bool CheckEnterPlayerInAttackZone(bool isEnter)
        {
            return enemyData.IsEnterAttackZone = isEnter;
        }
        #endregion
    }
}
