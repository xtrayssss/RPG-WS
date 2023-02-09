using Assets.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Character.Scripts
{
    public class Player : MonoBehaviour, IDamagable, IDying
    {
        #region Dependency
        public PlayerMove playerMove { get; set; }

        public FlipPlayer FlipPlayer { get; set; }

        private PlayerInputHandler player;

        private InjuredPlayer injuredPlayer;
        public AttackPlayer attackPlayer { get; private set; }

        private AnimationManager.AnimationManager animationManager;
        [field: SerializeField] public PlayerData.PlayerData PlayerData { get; private set; }
        
        [SerializeField] private Tilemap groundTileMap;
        
        [SerializeField] private Tilemap bridgeTileMap;

        [SerializeField] private Tilemap evilTileMap;

        private PlayerInputHandler inputHandler;

        private PlayerDeath playerDeath;

        private Collider2D _collider2D;

        [SerializeField] private Transform GFX;
        #endregion

        #region InitializeDependency
        private void InitializeDependency()
        {
            animationManager = GetComponent<AnimationManager.AnimationManager>();

            inputHandler = GetComponent<PlayerInputHandler>();

            player = GetComponent<PlayerInputHandler>();

            playerMove = new PlayerMove(transform, player, PlayerData, groundTileMap, bridgeTileMap, animationManager, this, evilTileMap);
            
            FlipPlayer = new FlipPlayer(transform, playerMove, inputHandler, GFX);

            injuredPlayer = new InjuredPlayer(PlayerData, animationManager);

            attackPlayer = new AttackPlayer(PlayerData, inputHandler, animationManager, this);

            playerDeath = new PlayerDeath(gameObject, _collider2D, groundTileMap);
        }
        #endregion

        #region Variables
        private State currentState = State.Idle;

        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();
        public Vector2 CurrentMoveInput { get; set; }
        [field: SerializeField] public GameObject prefabGrave { get; set; }
        
        [SerializeField] private float rayLength;

        [SerializeField] private Transform rightRay;
        [SerializeField] private Transform leftRay;
        [SerializeField] private Transform topRay;
        [SerializeField] private Transform downRay;
        [SerializeField] private LayerMask enemyMask;

        [field: SerializeField] public static bool[] DirectionsChecks { get; set; } = new bool[4];
        #endregion

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();

            InitializeDependency();

            Vector3Int randomPositionTile = StaticFunction.StaticFunction.GetRandomTileLeft(GenerateLevel.Instance.width / 2 - GenerateLevel.Instance.RandomShiftLeftWater, GenerateLevel.Instance.height);

            transform.position = groundTileMap.CellToLocal(randomPositionTile);

            PlayerData.CurrentHealth = PlayerData.MaxHealth;

            playerMove.targetPosition = transform.position;
        }

        private void Start()
        {
            playerMove.Initialize();
            
            attackPlayer.Initialize();  
        }
        private void Update()
        {
            FlipPlayer.Tick();
            Raycast();
            CheckState();
            //Debug.Log(inputHandler.moveInput);
            Debug.Log(currentState);
        }

        private void CheckState()
        {
            switch (currentState)
            {
                case State.Idle:
                    IdleState();
                    break;
                case State.Move:
                    animationManager.MoveAnimation();
                    playerMove.Tick(ref currentState);
                    break;
                case State.Attack:
                    AttackState();
                    break;
                case State.Hurt:
                    HurtState();
                    break;
                case State.Death:
                    Death(0);
                    break;
                default:
                    break;
            }
        }

        public void CheckRay(Vector3 startPosition, Vector3 targetPosition, float rayLength, LayerMask enemyMask, RaycastHit2D raycastHit2D, Direction enumDirection)
        {
            raycastHit2D = Physics2D.Raycast(startPosition, targetPosition, rayLength, enemyMask);

            if (raycastHit2D.collider != null)
            {
                DirectionsChecks[((int)enumDirection)] = true;
            }
            else
            {
                DirectionsChecks[((int)enumDirection)] = false;
            }
        }

        #region AcceptDamage
        public void AcceptDamage(int damage)
        {
            injuredPlayer.AcceptDamage(damage);

            currentState = State.Hurt;

            PlayerData.IsHurt = true;
        }
        #endregion

        #region IdleState
        public void IdleState()
        {
            animationManager.IdleAnimation();

            if (attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Attack;
            }

            if (inputHandler.moveInput != Vector2.zero && !attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Move;
            }

            if (PlayerData.IsHurt && player.moveInput == Vector2.zero)
            {
                currentState = State.Hurt;
            }
        }
        #endregion

        #region AttackState
        private void AttackState()
        {
            animationManager.AttackAnimation();

            if (!inputHandler.isAttack && inputHandler.moveInput == Vector2.zero)
            {
                currentState = State.Idle;
            }
            else if (!inputHandler.isAttack && inputHandler.moveInput != Vector2.zero)
            {
                currentState = State.Move;
            }
            else if (PlayerData.IsHurt)
            {
                currentState = State.Hurt;
                inputHandler.isAttack = false;
            }
        }
        #endregion

        #region DeathState
        public void Death(float destroyTime)
        {
            playerDeath.Death(destroyTime);
        }
        #endregion

        #region HurtState
        private void HurtState()
        {
            animationManager.HurtAnimation();

            if (attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Attack;
                PlayerData.IsHurt = false;
            }

            if (!PlayerData.IsHurt && inputHandler.moveInput != Vector2.zero && !attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Move;
            }

            if (!PlayerData.IsHurt && !inputHandler.isAttack && inputHandler.moveInput == Vector2.zero)
            {
                currentState = State.Idle;
            }

            if (PlayerData.CurrentHealth <= 0)
            {
                currentState = State.Death;
            }
        }
        #endregion

        #region MoveState
        private void MoveState()
        {
            if (PlayerData.IsHurt)
            {
                currentState = State.Hurt;
            }
        }
        #endregion

        public void Raycast()
        {
            RaycastHit2D raycastHitRight = new RaycastHit2D();

            CheckRay(rightRay.position, Vector3.right, rayLength, enemyMask, raycastHitRight, Direction.Right);

            
            RaycastHit2D raycastHitleft = new RaycastHit2D();

            CheckRay(leftRay.position, Vector3.left, rayLength, enemyMask, raycastHitleft, Direction.left);


            RaycastHit2D raycastHitTop = new RaycastHit2D();

            CheckRay(topRay.position, new Vector3(playerMove.DirectionPositionY, playerMove.DirectionPositionX - 0.4f), rayLength, enemyMask, raycastHitTop, Direction.Top);


            RaycastHit2D raycastHitDown = new RaycastHit2D();
           
            CheckRay(downRay.position, new Vector3(-playerMove.DirectionPositionY, -playerMove.DirectionPositionX), rayLength, enemyMask, raycastHitDown, Direction.Down);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(rightRay.position, Vector2.right * rayLength);
            Gizmos.DrawRay(leftRay.position, Vector2.left * rayLength);

            Gizmos.DrawRay(topRay.position, new Vector3(playerMove.DirectionPositionY, playerMove.DirectionPositionX - 0.4f));
            Gizmos.DrawRay(downRay.position, new Vector3(-playerMove.DirectionPositionY, -playerMove.DirectionPositionX));
        }
    }

}

public enum Direction
{
    Right,
    left,
    Top,
    Down
}

