using UnityEngine;

namespace Assets.Enemies.AnimationEventEnemy
{
    class AnimationEvents : MonoBehaviour
    {
        [SerializeField] BaseEntity.BaseEntity baseEntity;

        public void AttackForAnimationEvent()
        {
            baseEntity.TakeDamage();
        }
    }
}
