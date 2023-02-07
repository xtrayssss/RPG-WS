using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance { get; set; }
    [field: SerializeField] public bool isAttack { get; set; }
    public Vector2 moveInput {get; set;}
    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;
    }
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            isAttack = true;
        }
    }
    
}
