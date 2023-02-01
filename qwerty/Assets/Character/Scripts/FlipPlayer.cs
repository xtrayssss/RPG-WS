using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    [SerializeField] PlayerInputHandler inputHandler;
    private void Flip()
    {
        if (inputHandler.moveInput.x == -1)
        {
            transform.localScale = new Vector3(-0.0442966f, 0.0442966f, 0.0442966f);
        }
        if (inputHandler.moveInput.x == -1)
        {
            transform.localScale = new Vector3(0.0442966f, 0.0442966f, 0.0442966f);
        }
    }
}
