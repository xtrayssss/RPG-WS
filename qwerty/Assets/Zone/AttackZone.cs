using Assets.Zone.BaseZone;
using UnityEngine;
using System;

namespace Assets.Zone.AttackZone
{
    public class AttackZone : BaseZone.BaseZone
    {
        protected override void SomethingAction(Func<bool, bool> action, bool isEnter)
        {
            base.SomethingAction(baseEntity.CheckEnterPlayerInAttackZone, isEnter);
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
        }

    }
}
