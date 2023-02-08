using Assets.Interfaces;
using UnityEngine;

namespace Assets.Behavior
{
    class EnemyBehavior
    {
        private void DeathBehavior(IDying dying, float timeDestroy)
        {
            dying.Death(timeDestroy);
        }
    }
}
