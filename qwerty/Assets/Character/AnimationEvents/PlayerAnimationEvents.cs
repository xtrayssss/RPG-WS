using Assets.Character.Scripts;
using UnityEngine;

namespace Assets.Character.AnimationEvents
{
    class PlayerAnimationEvents : MonoBehaviour
    {
        private Player player;
        private PlayerInputHandler inputHandler;

        private void Awake()
        {
            player = GetComponentInParent<Player>();
            inputHandler = GetComponentInParent<PlayerInputHandler>();
        }
        public void OnAttack()
        {
            player.attackPlayer.Attack();
        }
        public void DoneAttack()
        {
            inputHandler.isAttack = false;
        }
    }
}
