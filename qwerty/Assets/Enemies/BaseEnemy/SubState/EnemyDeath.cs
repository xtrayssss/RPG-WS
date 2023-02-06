using UnityEngine;

namespace Assets.Enemies.BaseEnemy
{
    [System.Serializable]
    public class EnemyDeath
    {
        private GameObject currentObject;
        private BaseEntity.BaseEntity baseEntity;
        public EnemyDeath(BaseEntity.BaseEntity baseEntity,GameObject currentObject)
        {
            this.currentObject = currentObject;  
            this.baseEntity = baseEntity;
        }

        #region Death
        public void Death(float destroyTime, GameObject prefabGrave, Collider2D collider2D)
        {
            GameObject.Instantiate(prefabGrave, currentObject.transform.position, Quaternion.identity);
            GameObject.Destroy(baseEntity, destroyTime);
            GameObject.Destroy(currentObject, destroyTime);
            collider2D.enabled = false;
        }
        #endregion
    }
}
