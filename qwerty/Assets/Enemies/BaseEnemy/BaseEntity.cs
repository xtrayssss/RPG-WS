using UnityEngine;
using Assets.Enemies.AniamationManager;
using Assets.Character.Scripts;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        protected virtual void Awake()
        {
            _animation = GetComponent<EnemyAnimation>();
            targetPosition = transform.position;
        }
        protected virtual void Update()
        {
            SwitcherState();
            Debug.Log(currentState);
        }
        //protected virtual void FixedUpdate()
        //{
        //    if (currentState == State.Move)
        //    {
        //        Move();
        //    }
        //}
        private enum State
        {
            Idle,   
            Move,
            Attack,
            Hurt,
            Death,
        }
        private State currentState = State.Idle;

        [SerializeField] protected EnemyData enemyData;
        [SerializeField] protected PlayerInputHandler player;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected PlayerMove playerMove;
        
        [SerializeField] protected float timerStopAfterMove;
        [SerializeField] protected float totalStopAfterMove;
        protected bool hasMoved = true;
        protected AniamationManager.EnemyAnimation _animation;
        protected bool isIdle = false;
        protected bool isMove = false;
        protected bool isAttack = false;
        
        protected bool facingRight;

        private Vector2 targetPosition;

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
                case State.Move:
                    Move();
                    break;
                default:
                    break;
            }
        }
        protected virtual void Idle()
        {
            _animation.IdleAnimation();
            timerStopAfterMove = 0;
            if (enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
            
        }
        protected virtual void Move()
        {
            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
            if (enemyData.IsEnterAttackZone && (Vector2)transform.position == targetPosition)
            {
                 currentState = State.Attack;
            }

            if ((Vector2)transform.position == targetPosition)
            {
                timerStopAfterMove -= Time.deltaTime;
            }

            if (timerStopAfterMove <= 0)
            {
                targetPosition = (Vector2)transform.position + DirectionMove();
                timerStopAfterMove = totalStopAfterMove;
            }
            if (targetPosition != (Vector2)transform.position)
            {
                StepOnNextTile();
            }
            
            _animation.MoveAnimation();
            
            
            FlipEnemy();

            //if (enemyData.IsEnterAttackZone)
            //{
            //    currentState = State.Attack;
            //}
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

        #region StepOnNextTile
        protected void StepOnNextTile()
        {
             transform.position = Vector2.MoveTowards(
                 transform.position, 
                 targetPosition, 
                 2 * Time.deltaTime);
        }
        #endregion

        #region DirectionMove
        private Vector2 DirectionMove()
        {
            Vector2 direction = player.transform.position - transform.position;
           
            direction = direction.normalized;
            Debug.Log(direction);
            
            if (direction.x < 0.0f && playerMove.CurrentMoveInput.y == 0.0f)
            {
                return new Vector2(-0.72f,0);
            }
            else if (direction.x > 0.0f && playerMove.CurrentMoveInput.y == 0.0f)
            {
                return new Vector2(0.72f, 0);
            }
            else if (direction.y > 0.0f && playerMove.CurrentMoveInput.y != 0.0f)
            {
                return new Vector2(0, 0.81f);
            }
            else if (direction.y < 0.0f && playerMove.CurrentMoveInput.y != 0.0f)
            {
                return new Vector2(0, -0.81f);
            }
            else
            {
                return new Vector2(0f, 0f);
            }
        }
        #endregion

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
