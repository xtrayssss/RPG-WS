using Assets.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Character.Scripts
{
   public class AttackPlayer
    {
        // TODO: rewrite AttackPlayerClass
        
        private PlayerInputHandler inputHandler;

        private PlayerData.PlayerData playerData;

        private Player player;

        private AnimationManager.AnimationManager animationManager;

        public AttackPlayer(PlayerData.PlayerData playerData,
            PlayerInputHandler inputHandler, 
            AnimationManager.AnimationManager animationManager,
            Player player)
        {
            this.playerData = playerData;
            this.inputHandler = inputHandler;
            this.animationManager = animationManager;
            this.player = player;
        }
        public void Initialize()
        {
        }
        public AttackPlayer(){}
      
        public void Attack()
        {
            var filterList = player.hittObjects.Where(x => !x.isTrigger);

            foreach (var item in filterList)
            {
                item.GetComponent<IDamagable>()?.AcceptDamage(playerData.Damage);
                Debug.Log(item.name);
            }
        }

        public bool CheckAttack() => inputHandler.isAttack;
   }
}
