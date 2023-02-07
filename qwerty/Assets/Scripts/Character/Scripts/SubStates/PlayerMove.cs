using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Character.Scripts
{
    public class PlayerMove
    {
        #region Dependency
        public PlayerInputHandler player;
        private PlayerData.PlayerData playerData;
        private Tilemap groundTileMap;
        private Tilemap bridgeTileMap;
        private AnimationManager.AnimationManager animationManager;
        private Player playerBehavior;
        #endregion

        #region Variables
        private bool hasMoved { get; set; } = true;

        private Vector2 direction;
        public Vector3 targetPosition;
        private Vector3Int targetPositionInt;
        private float delaySeconds;
        private Transform currentTransform;
        private float totalDelaySeconds { get; set; } = 0.1f;

        public Vector2 CurrentMoveInput { get; set; }

        private const float DirectionPositionX = 0.72f;
        private const float DirectionPositionY = 0.81f - 0.18f;
        #endregion

        public PlayerMove(Transform currentTransform, PlayerInputHandler player,
            PlayerData.PlayerData playerData, Tilemap groundTileMap, 
            Tilemap bridgeTileMap, AnimationManager.AnimationManager animationManager,Player playerBehavior)
        {
            this.currentTransform = currentTransform;
            this.player = player;
            this.playerData = playerData;
            this.groundTileMap = groundTileMap;
            this.bridgeTileMap = bridgeTileMap;
            this.animationManager = animationManager;
            this.playerBehavior = playerBehavior;
        }
        public PlayerMove() { }

        #region Initialize
        public void Initialize()
        {
           
        }
        #endregion

        #region Tick
        public void Tick(ref State currentState)
        {
            SetTargetPosition();

            if (!hasMoved && !CheckCollision(targetPositionInt))
            {
                MovePlayer();
            }

            ReachedTargetPositon(ref currentState);
        }
        #endregion

        #region SetTargetPosition
        private void SetTargetPosition()
        {
            if (hasMoved && player.moveInput != Vector2.zero)
            {
                playerBehavior.CurrentMoveInput = player.moveInput;
                CurrentMoveInput = player.moveInput;
                SetDirection(ref direction);
                hasMoved = false;
                
                Vector3 directionMove = new Vector3(direction.x, direction.y, 0);

                targetPosition = currentTransform.position + directionMove;

                targetPositionInt = groundTileMap.WorldToCell(new Vector3(targetPosition.x, targetPosition.y, 1));
                

                if (CheckCollision(targetPositionInt))
                {
                    targetPosition = currentTransform.position;
                }
            }
        }
        #endregion

        #region ReachedTargetPositon
        private void ReachedTargetPositon(ref State currentState)
        {
            if (currentTransform.position == targetPosition)
            {
                delaySeconds -= Time.deltaTime;

                if (delaySeconds <= 0)
                {
                    delaySeconds = playerData.TotalDelaySeconds;
                    targetPosition = currentTransform.position;
                    hasMoved = true;

                    if (player.moveInput == Vector2.zero)
                    {
                        currentState = State.Idle;
                    }
                }
            }
        }
        #endregion

        #region MovePlayer
        public void MovePlayer() =>
            currentTransform.position =
                Vector2.MoveTowards(currentTransform.position,
                targetPosition,
                playerData.SpeedMove * Time.deltaTime);
        #endregion

        #region SetDirection
        private void SetDirection(ref Vector2 direction)
        {
            if (player.moveInput.x < 0.0f) direction = new Vector2(-DirectionPositionX, 0);

            if (player.moveInput.x > 0.0f) direction = new Vector2(DirectionPositionX, 0);

            if (player.moveInput.y < 0.0f) direction = new Vector2(-DirectionPositionX / 2.0f, -DirectionPositionY);

            if (player.moveInput.y > 0.0f) direction = new Vector2(DirectionPositionX / 2.0f, DirectionPositionY);
        }
        #endregion

        #region CheckCollision
        public bool CheckCollision(Vector3Int targetPositionInt)
        {
            return !groundTileMap.HasTile(targetPositionInt) && !bridgeTileMap.HasTile(targetPositionInt);
        }
        #endregion
    }
}
