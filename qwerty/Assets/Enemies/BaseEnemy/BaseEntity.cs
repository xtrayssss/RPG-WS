using UnityEngine;
using Assets.Enemies.AniamationManager;
using Assets.Character.Scripts;
using Unity.VisualScripting;
using Assets.Interfaces;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IDying
    {
        protected virtual void Awake()
        {

            transform.position = bridgeTileMap.WorldToCell(transform.position);
            playerMove = new PlayerMove();
            _animation = GetComponent<EnemyAnimation>();
            targetPosition = transform.position;
            
            enemyData.IsEnterAgroZone = false;
            enemyData.IsEnterAttackZone = false;
        }
        protected virtual void Update()
        {
            SwitcherState();
            //Debug.Log(currentState);
        }
        //protected virtual void FixedUpdate()
        //{
        //    if (currentState == State.Move)
        //    {
        //        Move();
        //    }
        //}

        private State currentState = State.Idle;

        [SerializeField] protected EnemyData enemyData;
        [SerializeField] protected PlayerInputHandler player;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
         protected PlayerMove playerMove;
        
        [SerializeField] protected float timerStopAfterMove;
        [SerializeField] protected float totalStopAfterMove;
        protected bool hasMoved = true;
        protected AniamationManager.EnemyAnimation _animation;
        protected bool isIdle = false;
        protected bool isMove = false;
        protected bool isAttack = false;
        
        protected bool facingRight;

        private Vector2 targetPosition;
        private Vector3Int targetPositionInt;

        private bool isIdleAnim;

        [SerializeField] private Tilemap groundTileMap;
        [SerializeField] private Tilemap bridgeTileMap;
        
        [SerializeField] private GameObject prefabGrave;

        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();
        
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
                    DieEnemy();
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
            // enemyMove.CheckNextState(targetPosition, currentState, State.Idle, State.Attack);

            //enemyMove.TimerTick(isIdleAnim, targetPosition, timerStopAfterMove, totalStopAfterMove);

            CheckNextState();

            TimerTick();

            if (targetPosition != (Vector2)transform.position)
            {
               StepOnNextTile(targetPosition);
            }
            if (!isIdleAnim)
            {
                _animation.MoveAnimation();
            }
            if (isIdleAnim)
            {
                _animation.IdleAnimation();
            }           
            
            FlipEnemy();
        }


        #region CheckNextState
        private void CheckNextState()
        {
            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
            if (enemyData.IsEnterAttackZone && (Vector2)transform.position == targetPosition)
            {
                currentState = State.Attack;
            }
        }
        #endregion

        #region TimerTick
        private void TimerTick()
        {
            if ((Vector2)transform.position == targetPosition)
            {
                timerStopAfterMove -= Time.deltaTime;
                isIdleAnim = true;
            }

            if (timerStopAfterMove <= 0)
            {
                targetPosition = (Vector2)transform.position + DirectionMove();

                targetPositionInt = groundTileMap.WorldToCell(new Vector3(targetPosition.x, targetPosition.y, 1));

                if (!groundTileMap.HasTile(targetPositionInt) && !bridgeTileMap.HasTile(targetPositionInt))
                {
                    targetPosition = transform.position;
                }

                timerStopAfterMove = totalStopAfterMove;

                isIdleAnim = false;
            }
        }
        #endregion

        #region Attack
        protected virtual void Attack()
        {
            _animation.AttackAnimation();
            
            FlipEnemy();

            if (!enemyData.IsEnterAttackZone && enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
            if (!enemyData.IsEnterAttackZone && !enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
        }
        #endregion

        protected virtual void Hurt()
        {
            if (CheckDeath())
            {
                currentState = State.Death;
            }
        }

        protected virtual void DieEnemy()
        {
            Death(0);
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

        #region AcceptDamage
        public void AcceptDamage(int damage)
        {
            enemyData.Health -= damage;
        }
        #endregion

        #region TakeDamage
        public void TakeDamage()
        {
            foreach (var item in hittObjects)
            {
                var hit = item.GetComponent<IDamagable>();

                hit?.AcceptDamage(enemyData.Damage);
            }
        }
        #endregion

        public void Death(float destroyTime)
        {
            Instantiate(prefabGrave, transform.position, Quaternion.identity);
            Destroy(this, destroyTime);
            Destroy(gameObject, destroyTime);
        }
        private bool CheckDeath()
        {
            return enemyData.Health <= 0;
        }
        public Vector2 DirectionMove()
        {
            Vector2 direction = player.transform.position - transform.position;

            direction = direction.normalized;
            Debug.Log(direction);

            if (direction.x < 0.0f && playerMove.CurrentMoveInput.y == 0.0f)
            {
                return new Vector2(-0.72f, 0);
            }
            else if (direction.x > 0.0f && playerMove.CurrentMoveInput.y == 0.0f)
            {
                return new Vector2(0.72f, 0);
            }
            else if (direction.y > 0.0f && playerMove.CurrentMoveInput.y != 0.0f)
            {
                return new Vector2(0, 0.81f + 0.405f);
            }
            else if (direction.y < 0.0f && playerMove.CurrentMoveInput.y != 0.0f)
            {
                return new Vector2(0, -(0.81f + 0.405f));
            }
            else
            {
                return new Vector2(0f, 0f);
            }
        }
        public void StepOnNextTile(Vector2 target)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                2 * Time.deltaTime);
        }
    }
}
