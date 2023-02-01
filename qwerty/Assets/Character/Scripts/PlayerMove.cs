using UnityEngine;
using Assets.Character.PlayerData;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerInputHandler player;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerData playerData;
    [SerializeField] Tilemap tilemap;
    public bool hasMoved { get; set; } = true;

    private Vector2 direction;
    private Vector3 targetPosition;

    private float delaySeconds;

    [field: SerializeField] private float totalDelaySeconds { get; set; } = 0.1f;

    private const float directionPositionX = 0.72f;
    private const float directionPositionY= 0.81f;

    private void Awake()
    {
        delaySeconds = totalDelaySeconds;
        
        Vector3Int startposition = Vector3Int.RoundToInt(transform.position);
        
        transform.position = tilemap.GetCellCenterWorld(startposition);
    }
    void Update()
    {
        SetTargetPosition();

        if (!hasMoved)
        {
            MovePlayer();
        }

        ReachedTargetPositon();
    }
    
    private void SetTargetPosition()
    {
        if (hasMoved && player.moveInput != Vector2.zero)
        {
            SetDirection(ref direction);
            hasMoved = false;
            Vector3 directionMove = new Vector3(direction.x, direction.y, 0);

            targetPosition = transform.position + directionMove;
        }
    }
    private void ReachedTargetPositon()
    {
        if (transform.position == targetPosition)
        {
            delaySeconds -= Time.deltaTime;

            if (delaySeconds <= 0)
            {
                delaySeconds = totalDelaySeconds;
                targetPosition = transform.position;
                hasMoved = true;
            }
        }
    }
    
    public void MovePlayer() => 
        transform.position = 
            Vector2.MoveTowards(transform.position,
            targetPosition,
            playerData.SpeedMove * Time.deltaTime);

    private void SetDirection(ref Vector2 direction)
    {
        if (player.moveInput.x < 0.0f) direction = new Vector2(-directionPositionX, 0);
        
        if (player.moveInput.x > 0.0f) direction = new Vector2(directionPositionX, 0);
       
        if (player.moveInput.y < 0.0f) direction = new Vector2(0, -directionPositionY);
        
        if (player.moveInput.y > 0.0f) direction = new Vector2(0, directionPositionY);
    }
}
