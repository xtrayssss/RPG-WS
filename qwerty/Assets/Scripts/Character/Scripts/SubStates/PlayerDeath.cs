using Assets.Interfaces;
using UnityEngine;

namespace Assets.Character.Scripts
{
    public class PlayerDeath : IDying
    {
        private GameObject playerGameOject;
        private Collider2D collider2D;
        public PlayerDeath(GameObject playerGameOject, Collider2D collider2D)
        {
            this.playerGameOject = playerGameOject;
            this.collider2D = collider2D;
        }

        public void DeathState(float destroyTime)
        {
            collider2D.enabled = false;

            if (CheckDestroyTimer(destroyTime)) playerGameOject.SetActive(false);

            //GameObject.Destroy(playerGameOject.GetComponent<Player>(), destroyTime); 
        }
        private bool CheckDestroyTimer(float destroyTime)
        {
            destroyTime -= Time.deltaTime;

            return destroyTime <= 0; 
        }
    }
}
