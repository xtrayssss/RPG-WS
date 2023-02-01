using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Character.PlayerData;
public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputHandler player;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerData playerData;

    void FixedUpdate()
    {
        MovePlayer();
    }
    public void MovePlayer()
    {
        if(player.moveInput.x == 1)
        {
           _rigidbody2D.position += new Vector2(0.256f + 0.256f * player.moveInput.x,0);    
        }
        if(player.moveInput.x == -1)
        {
            _rigidbody2D.position -= new Vector2(0.256f - 0.256f * player.moveInput.x,0);
        }
    }



}
