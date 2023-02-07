using Assets.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Character.Scripts
{
    public class Player : MonoBehaviour, IDamagable
    {
        #region Dependency
        private PlayerMove playerMove;

        private FlipPlayer flipPlayer;

        private PlayerInputHandler player;

        private InjuredPlayer injuredPlayer;
        public AttackPlayer attackPlayer { get; private set; }

        private AnimationManager.AnimationManager animationManager;
        
        [SerializeField] private PlayerData.PlayerData playerData;
        
        [SerializeField] private Tilemap groundTileMap;
        
        [SerializeField] private Tilemap bridgeTileMap;
        
        private PlayerInputHandler inputHandler;
        #endregion

        private State currentState = State.Idle;

        [HideInInspector] public List<Collider2D> hittObjects = new List<Collider2D>();
        public Vector2 CurrentMoveInput { get; set; }


        private void Awake()
        {
            transform.position =  StaticFunction.StaticFunction.SetPositionOnTheCenterTile(groundTileMap,transform.position);

            playerData.CurrentHealth = playerData.MaxHealth;
            
            animationManager = GetComponent<AnimationManager.AnimationManager>();
            
            inputHandler = GetComponent<PlayerInputHandler>();
            
            player = GetComponent<PlayerInputHandler>();

            playerMove = new PlayerMove(transform, player, playerData, groundTileMap, bridgeTileMap, animationManager, this);

            playerMove.targetPosition = transform.position;
            
            flipPlayer = new FlipPlayer(transform, playerMove);

            injuredPlayer = new InjuredPlayer(playerData, animationManager);

            attackPlayer = new AttackPlayer(playerData,inputHandler,animationManager, this);
        }

        private void Start()
        {
            playerMove.Initialize();
            
            attackPlayer.Initialize();  
        }
        private void Update()
        {
            flipPlayer.Tick();
            
            CheckState();
            
            if (attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Attack;
            }

            if (inputHandler.moveInput != Vector2.zero || playerMove.CurrentMoveInput != Vector2.zero && !attackPlayer.CheckAttack(transform.position, playerMove.targetPosition))
            {
                currentState = State.Move;
            }

            //Debug.Log(currentState);
        }

        private void CheckState()
        {
            switch (currentState)
            {
                case State.Idle:
                    animationManager.IdleAnimation();
                    break;
                case State.Move:
                    playerMove.Tick();
                    break;
                case State.Attack:
                    animationManager.AttackAnimation();
                    if (!inputHandler.isAttack && inputHandler.moveInput == Vector2.zero)
                    {
                        currentState = State.Idle;
                    }
                    else if (!inputHandler.isAttack && inputHandler.moveInput != Vector2.zero)
                    {
                        currentState = State.Move;
                    }
                    break;
                case State.Hurt:
                    break;
                case State.Death:
                    break;
                default:
                    break;
            }
        }

        public void AcceptDamage(int damage)
        {
            injuredPlayer.AcceptDamage(damage);

            animationManager.HurtAnimation();
        }
    }
}

