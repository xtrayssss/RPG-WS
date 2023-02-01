using Assets.Zone.BaseZone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroZone : BaseZone
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void SomethingAction(Func<bool, bool> action, bool isEnter)
    {
        base.SomethingAction(baseEntity.CheckEnterPlayerInAgroZone, isEnter);
    }
}
