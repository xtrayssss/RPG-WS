using UnityEngine;
using Assets.Enemies.AniamationManager;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        protected virtual void Awake()
        {
            _animation = GetComponent<EnemyAnimation>();
        }
        protected virtual void Update()
        {
            SwitcherState();
            Debug.Log(currentState);
        }
        protected virtual void FixedUpdate()
        {
            if (currentState == State.Move)
            {
                Move();
            }
        }
        private enum State
        {
            Idle,
            Move,
            Attack,
            Hurt,
            Death,
        }

        [SerializeField] protected EnemyData enemyData;
        [SerializeField] protected PlayerInputHandler player;
        [SerializeField] protected Rigidbody2D _rigidbody2D;

        protected AniamationManager.EnemyAnimation _animation;
        
        [SerializeField] protected float timer;

        private State currentState = State.Idle;
        
        protected bool isIdle = false;
        protected bool isMove = false;
        protected bool isAttack = false;
        
        protected bool facingRight;

        protected void SwitcherState()
        {
            switch (currentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Hurt:
                    Hurt();
                    break;
                case State.Death:
                    Death();
                    break;
                default:
                    break;
            }
        }
        protected virtual void Idle()
        {
            _animation.IdleAnimation();

            if (enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
        }
        protected virtual void Move()
        {
            _animation.MoveAnimation();

            DirectionMove();
            
            FlipEnemy();
            
            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
            if (enemyData.IsEnterAttackZone)
            {
                currentState = State.Attack;
            }
        }
        protected virtual void Attack()
        {
            _animation.AttackAnimation();

            if (!enemyData.IsEnterAttackZone && enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
            if (!enemyData.IsEnterAttackZone && !enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
        }

        protected virtual void Hurt()
        {
            
        }

        protected virtual void Death()
        {
            
        }

        #region CheckEnterPlayerInAgroZone
        public bool CheckEnterPlayerInAgroZone(bool isEnter)
        {
            return enemyData.IsEnterAgroZone = isEnter;
        }
        #endregion

        #region CheckEnterPlayerInAttackZone
        public bool CheckEnterPlayerInAttackZone(bool isEnter)
        {
            return enemyData.IsEnterAttackZone = isEnter;
        }
        #endregion

        protected void DirectionMove()
        {
            Vector2 direction = player.transform.position - transform.position;
            
            direction = direction.normalized;
            
            if (direction.x < 0.0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    _rigidbody2D.position += SetDirection(-0.72f, 0);
                    timer = 1;
                }
            }
            if (direction.x > 0.0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    _rigidbody2D.position += SetDirection(0.72f, 0);
                    timer = 1;
                }
            }
            if (direction.y < 0.0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    _rigidbody2D.position += SetDirection(0, -0.81f);
                    timer = 1;
                }
            }
            if (direction.y > 0.0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    _rigidbody2D.position += SetDirection(0, 0.81f);
                    timer = 1;
                }
            }
            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
        }

        #region SetDirection
        protected Vector2 SetDirection(float x, float y)
        {
            return new Vector2(x, y);
        }
        #endregion

        #region Flip
        protected void Flip()
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1.0f, transform.localScale.y);
        }
        #endregion

        #region FlipEnemy
        protected void FlipEnemy()
        {
            if (player.transform.position.x < transform.position.x && facingRight)
                Flip();
            if (player.transform.position.x > transform.position.x && !facingRight)
                Flip();
        }
        #endregion
    }
}
