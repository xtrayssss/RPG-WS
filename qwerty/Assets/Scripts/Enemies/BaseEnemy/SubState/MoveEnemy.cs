using Assets.Character.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Enemies.BaseEntity
{
    [System.Serializable]
    public class MoveEnemy
    {
        private Transform currentTransform;
        private PlayerInputHandler playerInputHandler;
        private AniamationManager.EnemyAnimation _animation;

        public Vector2 targetPosition;

        [SerializeField] private float timerStopAfterMove;
        public float totalStopAfterMove { get; set; }

        protected bool isIdle = false;
        protected bool isMove = false;
        protected bool isAttack = false;

        private bool isIdleAnim;
        private Vector3Int targetPositionInt;

        private Tilemap groundTileMap;
        private Tilemap bridgeTileMap;
        private EnemyData enemyData;

        private const float DIRECTION_X = 0.71f;
        private const float DIRECTION_Y = 0.81f - 0.20f;

        private State currentState;

        private Player player;

        public MoveEnemy(Transform currentTransform, PlayerInputHandler playerInputHandler,
            AniamationManager.EnemyAnimation _animation, Tilemap groundTileMap,
            Tilemap bridgeTileMap, EnemyData enemyData, ref State currentState,
            Player player)
        {
            this.currentTransform = currentTransform;
            this.playerInputHandler = playerInputHandler;
            this._animation = _animation;
            this.groundTileMap = groundTileMap;
            this.bridgeTileMap = bridgeTileMap;
            this.enemyData = enemyData;
            this.currentState = currentState;
            this.player = player;

        }
        public MoveEnemy() {}

        public void Initilize(float totalTimer)
        {
            totalStopAfterMove = totalTimer;
        }

        public void Tick()
        {
            TimerTick();


            if (CheckNextTargetPosition())
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
        }

        #region CheckNextState
        public void CheckNextState(ref State currentState)
        {
            if (!enemyData.IsEnterAgroZone)
            {
                currentState = State.Idle;
            }
            if (enemyData.IsEnterAttackZone &&
                (Vector2)currentTransform.position == targetPosition)
            {
                currentState = State.Attack;
            }
            if (enemyData.IsHurt)
            {
                currentState = State.Hurt;
            }
        }
        #endregion

        #region CheckNextTargetPosition
        private bool CheckNextTargetPosition()
        {
            return targetPosition != (Vector2)currentTransform.position;
        }
        #endregion

        #region TimerTick
        private void TimerTick()
        {
            if ((Vector2)currentTransform.position == targetPosition)
            {
                timerStopAfterMove -= Time.deltaTime;
                isIdleAnim = true;
            }

            if (timerStopAfterMove <= 0)
            {
                targetPosition = (Vector2)currentTransform.position + DirectionMove();

                // Debug.Log(DirectionMove());

                targetPositionInt = groundTileMap.WorldToCell(new Vector3(targetPosition.x, targetPosition.y, 1));

                if (!groundTileMap.HasTile(targetPositionInt) && !bridgeTileMap.HasTile(targetPositionInt))
                {
                    targetPosition = currentTransform.position;
                }

                timerStopAfterMove = totalStopAfterMove;

                isIdleAnim = false;
            }
        }
        #endregion

        #region DirectionMove
        public Vector2 DirectionMove()
        {
            Vector2 direction = playerInputHandler.transform.position - currentTransform.position;
            direction = direction.normalized;

            foreach (var item in Player.DirectionsChecks)
            {
                if (item)
                {
                    currentState = State.Attack;
                }
            }

            if (direction.x < 0.0f && direction.x < direction.y) return new Vector2(-DIRECTION_X, 0);

            else if (direction.x > 0.0f && direction.x > direction.y) return new Vector2(DIRECTION_X, 0);

            else if (direction.y > 0.0f && direction.y > direction.x) return new Vector2(DIRECTION_X / 2, DIRECTION_Y);

            else if (direction.y < 0.0f && direction.y < direction.x) return new Vector2(- DIRECTION_X / 2, -(DIRECTION_Y));

            else return new Vector2(0f, 0f);
        }
        #endregion
        
        #region StepOnNextTile
        public void StepOnNextTile(Vector2 target)
        {
            currentTransform.position = Vector2.MoveTowards(
                currentTransform.position,
                target,
                2 * Time.deltaTime);
        }
        #endregion
    }
}
