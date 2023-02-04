using Assets.Character.Scripts;
using UnityEngine;

namespace Assets.Enemies.BaseEntity.Move
{
    public class Move
    {
        private readonly Transform currentTransform;
        private readonly Transform targetTransform;
        private readonly PlayerMove playerMove;
        private readonly EnemyData enemyData;
        public Move(PlayerMove playerMove, Transform currentTransform, Transform targetTransform, EnemyData enemyData)
        {
            this.currentTransform = currentTransform;
            this.targetTransform = targetTransform;
            this.playerMove = playerMove;
            this.enemyData = enemyData;
        }
        public Vector2 DirectionMove()
        {
            Vector2 direction = targetTransform.transform.position - currentTransform.position;

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
                return new Vector2(0,0.81f + 0.405f);
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
            currentTransform.position = Vector2.MoveTowards(
                currentTransform.position,
                target,
                2 * Time.deltaTime);
        }
    }
}
