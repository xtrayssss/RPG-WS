using Assets.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Enemies.BaseEnemy
{
    [System.Serializable]
    public class EnemyDeath : IDying
    {
        private GameObject currentObject;
        private BaseEntity.BaseEntity baseEntity;
        private Tilemap groundTileMap;
        public EnemyDeath(BaseEntity.BaseEntity baseEntity, GameObject currentObject, Tilemap groundTileMap)
        {
            this.currentObject = currentObject;  
            this.baseEntity = baseEntity;
            this.groundTileMap = groundTileMap;
        }

        #region Death
        public void Death(float destroyTime)
        {
            Vector3 centerTilePosition = 
                StaticFunction.StaticFunction.SetPositionOnTheCenterTile(groundTileMap,
                currentObject.transform.position);

            SpawnGrave(baseEntity.prefabGrave, centerTilePosition);

            SpawnEffect(baseEntity.prefabEffectDeath, centerTilePosition);

            SetActiveComponent(currentObject.GetComponent<Collider2D>(), destroyTime);
        }
        #endregion

        public void SpawnEffect(GameObject prefabEffect, Vector3 position) => 
            baseEntity.GetComponent<IEffectAfterDeath>()?.InstantiateEffect(prefabEffect,
            position);

        public void SpawnGrave(GameObject prefabGrave, Vector3 position) =>
            GameObject.Instantiate(prefabGrave, position, Quaternion.identity);

        public void SetActiveComponent(Collider2D collider2D, float destroyTime)
        {
            currentObject.SetActive(false);

            collider2D.enabled = false;

            GameObject.Destroy(baseEntity, destroyTime);
        }
    }
}
