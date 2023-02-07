using UnityEngine;

namespace Assets.BaseAnim
{
    public class BaseAnimator : MonoBehaviour
    {
        protected Animator _animator;
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            
        }
        private void Awake()
        {
        }

        protected virtual string ANIM_BOOL_NAME_MOVE { get; set; } = "isMove";
        protected virtual string ANIM_BOOL_NAME_IDLE { get; set; } = "isIdle";
        protected virtual string ANIM_BOOL_NAME_ATTACK { get; set; } = "isAttack";
        protected virtual string ANIM_BOOL_NAME_HURT { get; set; } = "isHurt";

        protected virtual bool IS_MOVE { get; set; } = true;
        protected virtual bool IS_ATTACK { get; set; } = true;
        protected virtual bool IS_IDLE {get; set; } = true;
        protected virtual bool IS_HURT {get; set; } = true;

        public virtual void MoveAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_MOVE, IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);
            
            _animator.SetBool(ANIM_BOOL_NAME_HURT, !IS_HURT);

        }

        public virtual void AttackAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_HURT, !IS_HURT);

        }

        public virtual void IdleAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_IDLE, IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);
            
            _animator.SetBool(ANIM_BOOL_NAME_HURT, !IS_HURT);

        }

        public virtual void HurtAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_HURT, IS_HURT);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);
        }
    }
}
