using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Character.Scripts
{
    public class Player : MonoBehaviour
    {
        private PlayerMove playerMove;

        private FlipPlayer flipPlayer;

        private PlayerInputHandler player;

        private AnimationManager.AnimationManager animationManager;
        
        [SerializeField] private PlayerData.PlayerData playerData;
        [SerializeField] private Tilemap groundTileMap;
        [SerializeField] private Tilemap bridgeTileMap;

        private PlayerInputHandler inputHandler;

        private State currentState = State.Idle;

        private void Awake()
        { 
            animationManager = GetComponent<AnimationManager.AnimationManager>();
            
            inputHandler = GetComponent<PlayerInputHandler>();
            
            player = GetComponent<PlayerInputHandler>();

            playerMove = new PlayerMove(transform, player, playerData, groundTileMap, bridgeTileMap, animationManager);

            flipPlayer = new FlipPlayer(transform, playerMove);

            playerMove.Initialize();

        }

        private void Update()
        {
            flipPlayer.Tick();
            
            CheckState();

            if (inputHandler.moveInput != Vector2.zero || playerMove.CurrentMoveInput != Vector2.zero)
            {
                currentState = State.Move;
            }
            if (playerMove.targetPosition == transform.position)
            {
            }
            Debug.Log(currentState);

        }

        private void CheckState()
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Move:
                    playerMove.Tick();
                    break;
                case State.Attack:
                    break;
                case State.Hurt:
                    break;
                case State.Death:
                    break;
                default:
                    break;
            }
        }
    }
}

