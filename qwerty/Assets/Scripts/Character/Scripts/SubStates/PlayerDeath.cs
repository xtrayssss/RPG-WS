using Assets.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Character.Scripts
{
    public class PlayerDeath : IDying
    {
        private GameObject playerGameOject;
        private Collider2D collider2D;
        private Tilemap groundTileMap;
        public PlayerDeath(GameObject playerGameOject, Collider2D collider2D, Tilemap groundTileMap)
        {
            this.playerGameOject = playerGameOject;
            this.collider2D = collider2D;
            this.groundTileMap = groundTileMap;
        }

        public void Death(float destroyTime)
        {
            collider2D.enabled = false;

            Vector3 gravePosition = 
                StaticFunction.StaticFunction.SetPositionOnTheCenterTile(groundTileMap, 
                playerGameOject.transform.position);

            SpawnGrave(playerGameOject.GetComponent<Player>().prefabGrave, gravePosition);

            if (CheckDestroyTimer(destroyTime)) playerGameOject.SetActive(false);
        }
        private bool CheckDestroyTimer(float destroyTime)
        {
            destroyTime -= Time.deltaTime;

            return destroyTime <= 0; 
        }
        public void SpawnGrave(GameObject prefabGrave, Vector3 position) => 
            GameObject.Instantiate(prefabGrave, position, Quaternion.identity);
    }
}
