using Assets.Interfaces;
using UnityEngine;

namespace Assets.Character.Scripts
{
    public class InjuredPlayer :  IDamagable
    {
       private PlayerData.PlayerData playerData;

        private AnimationManager.AnimationManager animationManager;
        public InjuredPlayer(PlayerData.PlayerData playerData, AnimationManager.AnimationManager animationManager)
        {
            this.playerData = playerData;
            this.animationManager = animationManager;
        }
        public void AcceptDamage(int damage)
        {
            playerData.CurrentHealth -= damage;

            animationManager.HurtAnimation();

            //Debug.Log(playerData.CurrentHealth);
        }


    }
}
