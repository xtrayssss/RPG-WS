using Assets.Enemies.BaseEntity;
using Assets.Interfaces;
using System;
using UnityEngine;

namespace Assets.Zone.BaseZone
{
    public class BaseZone : MonoBehaviour
    {
        private Func<bool> ActionHandler;

        [SerializeField] protected BaseEntity baseEntity;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                SomethingAction(baseEntity.zoneCheckerEnemy.CheckEnterPlayerInAgroZone, true);
            }
        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                SomethingAction(baseEntity.zoneCheckerEnemy.CheckEnterPlayerInAgroZone, false);
            }
        }

        protected virtual void SomethingAction(Func<bool, bool> action, bool isEnter)
        {
            action?.Invoke(isEnter);
        }
    }
}
