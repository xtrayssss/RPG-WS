using Assets.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Enemies.BaseEnemy
{
    public class EnemyTakeDamage
    {
        private readonly EnemyData enemyData;
        private List<Collider2D> hittObjects;

        public EnemyTakeDamage(EnemyData enemyData, List<Collider2D> hittObjects)
        {
            this.enemyData = enemyData; 
            this.hittObjects = hittObjects;
        }

        #region TakeDamage
        public void TakeDamage()
        {
            var listColliders = hittObjects.Where(x => !x.isTrigger);

            foreach (var item in listColliders)
            {
                var hit = item.GetComponent<IDamagable>();

                if (hit != null)
                {
                    hit.AcceptDamage(enemyData.Damage);

                    //Debug.Log(item.name);
                }
            }
        }
        #endregion
    }
}
