using UnityEngine;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour
    {
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

        [SerializeField] protected Animator animator;
        [SerializeField] protected EnemyData enemyData;
        [SerializeField] protected PlayerInputHandler player;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected float timer;

        private State currentState = State.Idle;
        
        protected bool isIdle;
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
            animator.SetBool("isIdle", isIdle);

            if (enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
        }
        protected virtual void Move()
        {
            Vector2 direction = player.transform.position - transform.position;
            
            direction = direction.normalized;
            
            Debug.Log(direction);
            
            if (direction.x < 0.0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    _rigidbody2D.position += new Vector2(-0.72f, 0f);
                    timer = 1;
                }
            }
            if (direction.x > 0.0f)
            {
                timer -= Time.deltaTime;
                
                if (timer <= 0)
                {
                    _rigidbody2D.position += new Vector2(0.72f, 0f);
                    timer = 1;
                }
            }

            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
        }

        protected virtual void Attack()
        {
            
        }

        protected virtual void Hurt()
        {
            
        }

        protected virtual void Death()
        {
            
        }

        public bool CheckEnterPlayerInAgroZone(bool isEnter)
        {
            return enemyData.IsEnterAgroZone = isEnter;
        }
    }
}
