using Assets.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Character.Scripts
{
    class AttackPlayer
    {
        // TODO: rewrite AttackPlayerClass

        [SerializeField] private PlayerData.PlayerData playerData;
        
        private PlayerInputHandler inputHandler;

        public List<Collider2D> hitObjects = new List<Collider2D>();
      
        private void Attack()
        {
            foreach (var item in hitObjects)
            {
                item.GetComponent<IDamagable>()?.AcceptDamage(playerData.Damage);
                Debug.Log(item.name);
            }
        }
    }
}
