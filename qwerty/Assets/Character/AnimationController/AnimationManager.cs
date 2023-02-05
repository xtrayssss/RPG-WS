using UnityEngine;

namespace Assets.Character.Scripts.AnimationManager
{
   public class AnimationManager : BaseAnim.BaseAnimator
   {
        [SerializeField] private Animator animator;

        public override void AttackAnimation()
        {
            base.AttackAnimation();
        }

        public override void IdleAnimation()
        {
            base.IdleAnimation();
        }

        public override void MoveAnimation()
        {
            base.MoveAnimation();
        }

        public override void HurtAnimation()
        {
            base.HurtAnimation();
        }
    }
}
