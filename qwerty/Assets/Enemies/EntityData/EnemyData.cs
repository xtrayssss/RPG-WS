using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    private void Awake()
    {
        isEnterAgroZone = false;
        isEnterAttackZone = false;
    }

    [SerializeField] private float speedMove = 2.0f;

    [SerializeField] private int damage = 3;

    [SerializeField] private int health = 30;

    [SerializeField] private int maxHealth = 30;

    [SerializeField] private bool isEnterAgroZone;
   
    [SerializeField] private bool isEnterAttackZone;
    public float SpeedMove { get => speedMove; private set => speedMove = value; }
    public int Damage { get => damage; private set => damage = value; }
    public int Health { get => health; set => health = value; }
    public bool IsEnterAgroZone { get => isEnterAgroZone; set => isEnterAgroZone = value; }
    public bool IsEnterAttackZone { get => isEnterAttackZone; set => isEnterAttackZone = value; }
    public int MaxHealth { get => maxHealth; private set => maxHealth = value; }
}
