using Assets.Character.Scripts;
using Assets.Interfaces;
using Assets.Zone.BaseZone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Character.PlayerZone
{
    public class PlayerAttackZone : MonoBehaviour
    {
        private AttackPlayer attackPlayer;
        private void Awake()
        {
            attackPlayer = GetComponent<AttackPlayer>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            attackPlayer.hitObjects.Add(collision);
            
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            attackPlayer.hitObjects.Remove(collision);
        }
    }
}
