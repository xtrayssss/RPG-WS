using UnityEngine;
using Assets.Enemies.AniamationManager;
using Assets.Character.Scripts;
using Assets.Interfaces;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Assets.Enemies.BaseEnemy;
using System;

namespace Assets.Enemies.BaseEntity
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IDying
    {
        #region Variables

        [SerializeField] private Tilemap groundTileMap;
        [SerializeField] private Tilemap bridgeTileMap;
        [SerializeField] protected float totalStopAfterMove;
        public GameObject prefabGrave;
        public GameObject prefabEffectDeath;
        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();
        protected Collider2D _collider2D;
       
        private State currentState = State.Idle;
        #endregion

        #region Dependency
        private Player playerBehavior;

        [SerializeField] protected MoveEnemy moveEnemy;

        private FlipEnemy flipEnemy;

        [field: SerializeField] public EnemyData EnemyData { get; private set; }
       
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
            enemyAccepted = new EnemyAcceptedDamage(EnemyData, _animation);
            player = FindObjectOfType<PlayerInputHandler>();
            playerMove = new PlayerMove();
            playerBehavior = FindObjectOfType<Player>();
            moveEnemy = new MoveEnemy(transform, player, _animation, groundTileMap, bridgeTileMap, EnemyData, ref currentState, playerBehavior);
            flipEnemy = new FlipEnemy(transform, playerBehavior);
            zoneCheckerEnemy = new ZoneCheckerEnemy(EnemyData);
            EnemyTakeDamage = new EnemyTakeDamage(EnemyData, hittObjects);
            EnemyDeath = new EnemyDeath(this, gameObject, groundTileMap);
        }
        #endregion

        #region Init
        private void Init()
        {
            InitializeDependency();
            
            EnemyData.Health = EnemyData.MaxHealth;

            _collider2D = GetComponent<Collider2D>();


            _animation = GetComponent<EnemyAnimation>();

            EnemyData.IsEnterAgroZone = false;

            EnemyData.IsEnterAttackZone = false;

            EnemyData.IsHurt = false;

            moveEnemy.Initilize(totalStopAfterMove);
        }
        #endregion

        protected virtual void Awake()
        {
            Vector3Int randomPositionTile = StaticFunction.StaticFunction.GetRandomTileRight(GenerateLevel.Instance.width, GenerateLevel.Instance.height);

            transform.position = groundTileMap.CellToLocal(randomPositionTile);

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
            
            if (EnemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }

            if (EnemyData.IsHurt)
            {
                currentState = State.Hurt;
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

            if (EnemyData.IsHurt)
            {
                currentState = State.Hurt;
            }
            if (!EnemyData.IsEnterAttackZone && EnemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
            if (!EnemyData.IsEnterAttackZone && !EnemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
        }
        #endregion

        #region HurtState
        protected virtual void Hurt()
        {
            _animation.HurtAnimation();

            if (CheckDeath())
            {
                currentState = State.Death;
            }
            if (!EnemyData.IsHurt && EnemyData.IsEnterAgroZone)
            {
                currentState = State.Move;
            }
            if (!EnemyData.IsHurt && EnemyData.IsEnterAttackZone)
            {
                currentState = State.Attack;
            }
            if (!EnemyData.IsHurt && !EnemyData.IsEnterAttackZone && !EnemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
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
            EnemyData.IsHurt = true;
            currentState = State.Hurt;
        }
        #endregion

        #region CheckDeath
        private bool CheckDeath()
        {
            return EnemyData.Health <= 0;
        }
        #endregion

        #region DeathState
        public void Death(float destroyTime)
        {
            EnemyDeath.Death(destroyTime);
        }
        #endregion
    }
}
