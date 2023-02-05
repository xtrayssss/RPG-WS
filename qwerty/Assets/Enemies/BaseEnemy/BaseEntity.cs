using UnityEngine;
using Assets.Enemies.AniamationManager;
using Assets.Character.Scripts;
using Assets.Interfaces;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IDying
    {
        private Player playerBehavior;

        [SerializeField] protected MoveEnemy moveEnemy;

        private void Init()
        {
            Vector3Int startPosition = groundTileMap.WorldToCell(transform.position);

            transform.position = groundTileMap.CellToLocal(startPosition);

            enemyData.Health = enemyData.MaxHealth;

            player = FindObjectOfType<PlayerInputHandler>();

            playerMove = new PlayerMove();

            _animation = GetComponent<EnemyAnimation>();

            enemyData.IsEnterAgroZone = false;
            enemyData.IsEnterAttackZone = false;

            playerBehavior = FindObjectOfType<Player>();

            moveEnemy = new MoveEnemy(transform, player, _animation, groundTileMap, bridgeTileMap, enemyData);

            moveEnemy.Initilize(totalStopAfterMove);

        }

        protected virtual void Awake()
        {
            Init();
        }
        protected virtual void Update()
        {
            SwitcherState(); 
        }

        private State currentState = State.Idle;

        [SerializeField] protected EnemyData enemyData;
         protected PlayerInputHandler player;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
         protected PlayerMove playerMove;
        
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
        [SerializeField] protected float totalStopAfterMove;
        [SerializeField] private GameObject prefabGrave;

        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();

        #region SwitcherState
        protected void SwitcherState()
        {
            Hurt();
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
        #endregion

        protected virtual void Idle()
        {
            _animation.IdleAnimation();
            //timerStopAfterMove = 0;
            
            if (enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
        }

        protected virtual void Move()
        {
            //CheckNextState();
            
            
            moveEnemy.Tick();
            moveEnemy.CheckNextState(ref currentState);
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
            Debug.Log(enemyData.Health);
        }
        #endregion

        #region TakeDamage
        public void TakeDamage()
        {
            var listColliders = hittObjects.Where(x => !x.isTrigger);


            foreach (var item in listColliders)
            {
                var hit = item.GetComponent<IDamagable>();

                if (hit != null)
                {
                    hit.AcceptDamage(enemyData.Damage);

                    //Debug.Log(item.name);
                }
            }
        }
        #endregion

        #region Death
        public void Death(float destroyTime)
        {
            Instantiate(prefabGrave, transform.position, Quaternion.identity);
            Destroy(this, destroyTime);
            Destroy(gameObject, destroyTime);
        }
        #endregion
        private bool CheckDeath()
        {
            return enemyData.Health <= 0;
        }

       
    }
}
