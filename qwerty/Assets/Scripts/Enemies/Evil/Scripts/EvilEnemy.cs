using Assets.Enemies.BaseEntity;
using Assets.Interfaces;
using UnityEngine;

public class EvilEnemy : BaseEntity, IEffectAfterDeath
{
    public void InstantiateEffect(GameObject prefab, Vector3 position) =>
        Instantiate(prefab, position, Quaternion.identity);

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DieEnemy()
    {
        base.DieEnemy();
    }

    protected override void Hurt()
    {
        base.Hurt();
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Update()
    {
        base.Update();
    }
}
