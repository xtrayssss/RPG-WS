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
        private const float DirectionPositionY = 0.81f + 0.405f;
        #endregion

        public PlayerMove(Transform currentTransform, PlayerInputHandler player,
            PlayerData.PlayerData playerData, Tilemap groundTileMap, 
            Tilemap bridgeTileMap, AnimationManager.AnimationManager animationManager)
        {
            this.currentTransform = currentTransform;
            this.player = player;
            this.playerData = playerData;
            this.groundTileMap = groundTileMap;
            this.bridgeTileMap = bridgeTileMap;
            this.animationManager = animationManager;
        }
        public PlayerMove() { }

        #region Initialize
        public void Initialize()
        {
            Vector3Int startposition = Vector3Int.RoundToInt(currentTransform.position);

            currentTransform.position = groundTileMap.GetCellCenterWorld(startposition);
        }
        #endregion

        #region Tick
        public void Tick()
        {
            SetTargetPosition();

            if (!hasMoved && !CheckCollision(targetPositionInt))
            {
                MovePlayer();
                animationManager.MoveAnimation();       
            }

            ReachedTargetPositon();
        }
        #endregion

        #region SetTargetPosition
        private void SetTargetPosition()
        {
            if (hasMoved && player.moveInput != Vector2.zero)
            {
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
        private void ReachedTargetPositon()
        {
            if (currentTransform.position == targetPosition)
            {
                delaySeconds -= Time.deltaTime;

                if (delaySeconds <= 0)
                {
                    delaySeconds = totalDelaySeconds;
                    targetPosition = currentTransform.position;
                    hasMoved = true;
                    animationManager.IdleAnimation();
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

            if (player.moveInput.y < 0.0f) direction = new Vector2(0, -DirectionPositionY);

            if (player.moveInput.y > 0.0f) direction = new Vector2(0, DirectionPositionY);
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
