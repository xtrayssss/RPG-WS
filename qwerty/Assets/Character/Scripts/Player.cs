using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Character.PlayerData;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputHandler player;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerData playerData;

    private Vector2 direction;

    public bool hasMoved { get; set; } = true;
    //public bool canMoved { get; set; } = false;

    //private Vector3 targetPosition;

    //private Vector3 nextMovePosition;
    private Vector3 currentPosition;
    //[SerializeField] Tilemap tilemap;
    
    //Vector2Int Vector2Int;
    //Vector2 startPosition;
    
    float secondsMove;
    float totalAmountSecondsMove = 1f;
    private void Awake()
    {
        secondsMove = totalAmountSecondsMove;

        //transform.position = new Vector3(0.6f, 0.4f, 0);

        currentPosition = transform.position;

        //targetPosition = new Vector3(transform.position.x + 0.72f, transform.position.y, 0);
        
    }
    void Update()
    {
        if (player.moveInput.x != 0.0f && hasMoved)
        {
            hasMoved = true;
            SetDirection(ref direction);
        }

        secondsMove -= Time.deltaTime;

        if (hasMoved)
        {
            transform.position = MovePlayer();

            secondsMove = totalAmountSecondsMove;
        }    
    }

    public Vector3 MovePlayer()
    {
        return Vector2.MoveTowards(transform.position, currentPosition + new Vector3(direction.x, direction.y, 0), 2.0f * Time.deltaTime);

        
    }
    private void SetDirection(ref Vector2 direction)
    {
        if (player.moveInput.x < 0.0f)
        {
            direction = new Vector2(-0.72f, 0);
        }
        if (player.moveInput.x > 0.0f)
        {
            direction = new Vector2(0.72f, 0);
        }
    }
    //    if (player.moveInput.x < 0.0f && hasMoved)
    //    {
    //        targetPosition = Vector2.zero;

    //        targetPosition = new Vector3(transform.position.x - 0.72f, transform.position.y, 0);

    //        transform.localScale = new Vector2(-0.0442966f, 0.0442966f);
    //    }
    //    if (player.moveInput.x > 0.0f)
    //    {
    //        transform.localScale = new Vector2(0.0442966f, 0.0442966f);
    //    }
    //    //Debug.Log(targetPosition);

    //    if (player.moveInput != Vector2.zero && transform.position.x == targetPosition.x)
    //    {
    //        hasMoved = false;
    //        canMoved = false;
    //        SetDirection(ref direction);
    //    }

    //    if (!hasMoved &&  transform.position.x != targetPosition.x)
    //    {
    //        transform.position = MovePlayer();
    //    }

    //    if (!hasMoved)
    //    {
    //        secondsMove -= Time.deltaTime;
    //    }

    //    if (transform.position.x == targetPosition.x)
    //    {
    //        canMoved = true;
    //    }

    //    if (transform.position.x == targetPosition.x && secondsMove <= 0)
    //    {
    //        hasMoved = true;

    //        targetPosition = new Vector3(transform.position.x + direction.x, transform.position.y, 0);

    //        currentPosition = transform.position;

    //        Debug.Log("sdfsdf");

    //        secondsMove = totalAmountSecondsMove;
    //    }
    //}
    //public Vector3 MovePlayer()
    //{
    //    return Vector2.MoveTowards(transform.position, currentPosition + new Vector3(direction.x, direction.y, 0), 2.0f * Time.deltaTime);
    //}




}
