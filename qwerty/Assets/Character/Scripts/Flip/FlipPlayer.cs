using UnityEngine;

namespace Assets.Character.Scripts
{
    [RequireComponent(typeof(PlayerInputHandler), typeof(PlayerMove))]
    public class FlipPlayer : MonoBehaviour
    {
        [SerializeField] PlayerMove playerMove;

        private const float ScaleSizePlayer = 0.0442966f;

        private void Update()
        {
            Flip();
        }
        private void Flip()
        {
            if (playerMove.CurrentMoveInput.x  <= 0.0f)
            {
                transform.localScale = new Vector3(-ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
            if (playerMove.CurrentMoveInput.x >= 0.0f)
            {
                transform.localScale = new Vector3(ScaleSizePlayer, ScaleSizePlayer, ScaleSizePlayer);
            }
        }
    }
}