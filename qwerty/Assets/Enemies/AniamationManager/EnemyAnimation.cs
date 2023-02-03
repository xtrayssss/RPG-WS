using UnityEngine;

namespace Assets.Enemies.AniamationManager
{
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        private const string ANIM_BOOL_NAME_MOVE = "isMove";
        private const string ANIM_BOOL_NAME_IDLE = "isIdle";
        private const string ANIM_BOOL_NAME_ATTACK = "isAttack";

        private const bool IS_MOVE = true;
        private const bool IS_ATTACK = true;
        private const bool IS_IDLE = true;
        public void MoveAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_MOVE, IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);

            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);
        }

        public void AttackAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, IS_ATTACK);
            
            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);

            _animator.SetBool(ANIM_BOOL_NAME_IDLE, !IS_IDLE);
        }

        public void IdleAnimation()
        {
            _animator.SetBool(ANIM_BOOL_NAME_IDLE, IS_IDLE);
            
            _animator.SetBool(ANIM_BOOL_NAME_ATTACK, !IS_ATTACK);

            _animator.SetBool(ANIM_BOOL_NAME_MOVE, !IS_MOVE);
        }
    }
}
