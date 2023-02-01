using UnityEngine;

namespace Assets.Character.Scripts.AnimationManager
{
    [RequireComponent(typeof(PlayerInputHandler), typeof(Animator))]
    class AnimationManager : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [SerializeField] private PlayerMove player;

        [SerializeField] private float totalTimerIdleState;
        
        private float timerIdleState;

        private const bool IsMove = false;

        private const string AnimBoolName = "isMove";

        private void Awake()
        {
            timerIdleState = totalTimerIdleState;
        }
        private void Update()
        {
            AnimationMovePlayer();
        }

        private void AnimationMovePlayer()
        {
            if (!player.hasMoved || player.player.moveInput == Vector2.zero)
            {
                timerIdleState -= Time.deltaTime;
            }
            if (timerIdleState <= 0 && player.player.moveInput == Vector2.zero)
            {
                animator.SetBool(AnimBoolName, IsMove);

                timerIdleState = totalTimerIdleState;
            }
            if (!player.hasMoved)
            {
                animator.SetBool(AnimBoolName, !IsMove);
            }
        }
    }
}
