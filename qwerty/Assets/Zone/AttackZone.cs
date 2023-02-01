using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Warrior_1")
        {
            Debug.Log("collsion");
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}
