using UnityEngine;

namespace Assets.Character.Scripts
{
    public class FlipPlayer 
    {

        #region Dependency
        private PlayerMove playerMove;
        private Transform currentTransform;
        #endregion

        #region Varibles
        private const float ScaleSizePlayer = 0.0442966f;
        #endregion

        public FlipPlayer(Transform currentTransform ,PlayerMove playerMove)
        {
            this.playerMove = playerMove;
            this.currentTransform = currentTransform;
        }
        public void Tick()
        {
            Flip();
        }

        #region Flip
        private void Flip()
        {
            if (playerMove.CurrentMoveInput.x <= 0.0f)
            {
                currentTransform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.x >= 0.0f)
            {
                currentTransform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
        }
        #endregion
    }
}