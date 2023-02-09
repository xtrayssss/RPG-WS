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
        private const float ScaleSizePlayer = 1f;

        private Transform rayRight;
        private Transform rayLeft;
        private Transform GFX;
        #endregion

        public FlipPlayer(Transform currentTransform ,PlayerMove playerMove, 
            PlayerInputHandler playerInputHandler, Transform GFX)
        {
            this.playerMove = playerMove;
            this.currentTransform = currentTransform;
            this.playerInputHandler = playerInputHandler;
            this.GFX = GFX;
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
                GFX.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                return;
            }
            if (playerMove.CurrentMoveInput.x < 0.0f && playerMove.CurrentMoveInput.y > 0.0f)
            {
                GFX.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                return;
            }
            if (playerMove.CurrentMoveInput.y < 0.0f)
            {
                GFX.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.y > 0.0f)
            {
                GFX.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.x < 0.0f)
            {
                GFX.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                
                //rayLeft.transform.position =
                //    new Vector3(-rayLeft.transform.position.x, rayLeft.transform.position.y);  
            }
            if (playerMove.CurrentMoveInput.x > 0.0f)
            {
                GFX.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
                
                //rayRight.transform.position =
                //    new Vector3(-rayRight.transform.position.x, rayRight.transform.position.y);
            }
        }
        #endregion
    }
}