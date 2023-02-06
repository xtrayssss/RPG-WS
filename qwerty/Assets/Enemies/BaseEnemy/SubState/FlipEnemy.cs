using Assets.Character.Scripts;
using UnityEngine;

namespace Assets.Enemies.BaseEntity
{
    public class FlipEnemy 
    {
        #region Dependency
        private Transform currentTransform;
        private Player player;
        #endregion

        #region Variables
        private bool facingRight;
        #endregion


        public FlipEnemy(Transform currentTransform, Player player)
        {
            this.currentTransform = currentTransform;
            this.player = player;   
        }

        #region FlipEnemy
        public void FlipRealize()
        {
            if (player.transform.position.x < currentTransform.position.x && facingRight)
                Flip();
            if (player.transform.position.x > currentTransform.position.x && !facingRight)
                Flip();
        }
        #endregion

        #region Flip
        protected void Flip()
        {
            facingRight = !facingRight;
            currentTransform.localScale = new Vector2(currentTransform.localScale.x * -1.0f, currentTransform.localScale.y);
        }
        #endregion
    }
}