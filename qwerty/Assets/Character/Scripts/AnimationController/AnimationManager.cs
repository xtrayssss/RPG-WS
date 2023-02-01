using UnityEngine;

namespace Assets.Character.Scripts.AnimationManager
{
    class AnimationManager : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [SerializeField] private PlayerMove player;

        private void Update()
        {
            animator.SetBool("isMove", !player.hasMoved);
        }

    }
}
