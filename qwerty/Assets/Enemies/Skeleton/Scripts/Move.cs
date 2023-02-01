using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speedMove = 2.0f;
    
    [SerializeField] Rigidbody2D _rigidbody2D;
    void FixedUpdate()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
           
        Vector2 direction = player.transform.position - transform.position;
        _rigidbody2D.velocity = direction.normalized * speedMove * Time.fixedDeltaTime;
    }
}
