using UnityEngine;
using Assets.Enemies.AniamationManager;
using Assets.Character.Scripts;
using Assets.Interfaces;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Assets.Enemies.BaseEnemy;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IDying
    {
        #region Variables

        [SerializeField] private Tilemap groundTileMap;
        [SerializeField] private Tilemap bridgeTileMap;
        [SerializeField] protected float totalStopAfterMove;
        public GameObject prefabGrave;
        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();
        protected Collider2D _collider2D;
       
        private State currentState = State.Idle;
        #endregion

        #region Dependency
        private Player playerBehavior;

        [SerializeField] protected MoveEnemy moveEnemy;

        private FlipEnemy flipEnemy;

        [SerializeField] protected EnemyData enemyData;
       
        protected PlayerInputHandler player;
       
        protected PlayerMove playerMove;
       
        protected AniamationManager.EnemyAnimation _animation;
        public ZoneCheckerEnemy zoneCheckerEnemy { get; set; }

        private EnemyAcceptedDamage enemyAccepted;
        public EnemyTakeDamage EnemyTakeDamage { get; set; }
        [field: SerializeField] public EnemyDeath EnemyDeath { get; set; }
        #endregion

        #region InitializeDependency
        private void InitializeDependency()
        {
            _animation = GetComponent<AniamationManager.EnemyAnimation>();
            player = FindObjectOfType<PlayerInputHandler>();
            playerMove = new PlayerMove();
            playerBehavior = FindObjectOfType<Player>();
            moveEnemy = new MoveEnemy(transform, player, _animation, groundTileMap, bridgeTileMap, enemyData);
            flipEnemy = new FlipEnemy(transform, playerBehavior);
            zoneCheckerEnemy = new ZoneCheckerEnemy(enemyData);
            enemyAccepted = new EnemyAcceptedDamage(enemyData);
            EnemyTakeDamage = new EnemyTakeDamage(enemyData, hittObjects);
            EnemyDeath = new EnemyDeath(this, gameObject);
        }
        #endregion

        #region Init
        private void Init()
        {
            InitializeDependency();

            Vector3Int startPosition = groundTileMap.WorldToCell(transform.position);

            transform.position = groundTileMap.CellToLocal(startPosition);

            enemyData.Health = enemyData.MaxHealth;

            _collider2D = GetComponent<Collider2D>();


            _animation = GetComponent<EnemyAnimation>();

            enemyData.IsEnterAgroZone = false;

            enemyData.IsEnterAttackZone = false;

            moveEnemy.Initilize(totalStopAfterMove);
        }
        #endregion

        protected virtual void Awake()
        {
            Init();
        }
        protected virtual void Update()
        {
            SwitcherState();

            flipEnemy.FlipRealize();
        }

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

        #region IdleState
        protected virtual void Idle()
        {
            _animation.IdleAnimation();
            
            if (enemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
        }
        #endregion

        #region MoveState
        protected virtual void Move()
        {
            moveEnemy.Tick();
            moveEnemy.CheckNextState(ref currentState);
        }
        #endregion

        #region AttackState
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
        #endregion

        #region HurtState
        protected virtual void Hurt()
        {
            if (CheckDeath())
            {
                currentState = State.Death;
            }
        }
        #endregion

        #region DieEnemyState
        protected virtual void DieEnemy()
        {
            Death(0);
        }
        #endregion
       
        #region AcceptDamage
        public void AcceptDamage(int damage)
        {
            enemyAccepted.AcceptDamage(damage);
        }
        #endregion

        private bool CheckDeath()
        {
            return enemyData.Health <= 0;
        }

        #region DeathState
        public void Death(float destroyTime)
        {
            EnemyDeath.Death(destroyTime, prefabGrave, _collider2D);
        }
        #endregion
    }
}
