using UnityEngine;


namespace Assets.BaseAnimator
{
    public class BaseAnimator : MonoBehaviour
    {
        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        protected const string ANIM_BOOL_NAME_MOVE = "isMove";
        protected const string ANIM_BOOL_NAME_IDLE = "isIdle";
        protected const string ANIM_BOOL_NAME_ATTACK = "isAttack";

        protected const bool IS_MOVE = true;
        protected const bool IS_ATTACK = true;
        protected const bool IS_IDLE = true;

        public virtual void MoveAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_MOVE, IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);
        }

        public virtual void AttackAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);
        }

        public virtual void IdleAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_IDLE, IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);
        }
    }
}
