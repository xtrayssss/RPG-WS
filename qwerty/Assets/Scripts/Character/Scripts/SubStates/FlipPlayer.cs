using UnityEngine;

namespace Assets.Character.Scripts
{
    public class FlipPlayer 
    {

        #region Dependency
        private PlayerMove playerMove;
        private Transform currentTransform;
        private PlayerInputHandler playerInputHandler;
        #endregion

        #region Varibles
        private const float ScaleSizePlayer = 0.04f;
        #endregion

        public FlipPlayer(Transform currentTransform ,PlayerMove playerMove, PlayerInputHandler playerInputHandler)
        {
            this.playerMove = playerMove;
            this.currentTransform = currentTransform;
            this.playerInputHandler = playerInputHandler;
        }
        public void Tick()
        {
            Flip();
        }

        #region Flip
        private void Flip()
        {
            if (playerMove.CurrentMoveInput.x > 0.0f && playerMove.CurrentMoveInput.y < 0.0f)
            {
                currentTransform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                return;
            }
            if (playerMove.CurrentMoveInput.x < 0.0f && playerMove.CurrentMoveInput.y > 0.0f)
            {
                currentTransform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                return;
            }
            if (playerMove.CurrentMoveInput.y < 0.0f)
            {
                currentTransform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.y > 0.0f)
            {
                currentTransform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.x < 0.0f)
            {
                currentTransform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.x > 0.0f)
            {
                currentTransform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
        }
        #endregion
    }
}