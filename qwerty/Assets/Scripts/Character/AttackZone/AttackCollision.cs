using Assets.Character.Scripts;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Player player;

    private const string TAG_ENEMY = "Enemy";
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == TAG_ENEMY)
        {
            if (!player.hittObjects.Contains(collision))
            {
                player.hittObjects.Add(collision);
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
