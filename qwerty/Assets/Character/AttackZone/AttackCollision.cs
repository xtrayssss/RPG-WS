using Assets.Character.Scripts;
using UnityEngine;
using System.Linq;

public class AttackCollision : MonoBehaviour
{
    private AttackPlayer attackPlayer;
    private Player player;

    private const string TAG_ENEMY = "Enemy";
    private void Awake()
    {
        attackPlayer = new AttackPlayer();
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var hittable = collision.GetComponent<BaseEntity>() as IDamagable;

        if (collision.tag == TAG_ENEMY)
        {
            player.hittObjects.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       // var hittable = collision.GetComponent<BaseEntity>() as IDamagable;

        if (collision.tag == TAG_ENEMY)
        {
            //attackPlayer.hitObjects.Clear();

            Debug.Log("exit");
        }
    }
}
