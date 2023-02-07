using Assets.Character.Scripts;
using UnityEngine;

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == TAG_ENEMY)
        {
            if (!player.hittObjects.Contains(collision))
            {
                player.hittObjects.Add(collision);
                Debug.Log(123);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TAG_ENEMY)
        {
            player.hittObjects.Clear();
        }
    }
}
