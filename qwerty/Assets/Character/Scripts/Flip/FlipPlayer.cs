using UnityEngine;

namespace Assets.Character.Scripts
{
    [RequireComponent(typeof(PlayerInputHandler), typeof(PlayerMove))]
    public class FlipPlayer : MonoBehaviour
    {
        [SerializeField] PlayerInputHandler inputHandler;
        [SerializeField] PlayerMove playerMove;

        private const float ScaleSizePlayer = 0.0442966f;

        private void Update()
        {
            Flip();
        }
        private void Flip()
        {
            if (inputHandler.moveInput.x == -1 && playerMove.hasMoved)
            {
                transform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (inputHandler.moveInput.x == 1 && playerMove.hasMoved)
            {
                transform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
        }
    }
}