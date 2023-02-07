using UnityEngine;

namespace Assets.Enemies.AnimationEventEnemy
{
    class AnimationEvents : MonoBehaviour
    {
        [SerializeField] BaseEntity.BaseEntity baseEntity;

        public void AttackForAnimationEvent()
        {
            baseEntity.EnemyTakeDamage.TakeDamage();
        }
        public void HurtStart()
        {
            baseEntity.EnemyData.IsHurt = true;
        }
        public void HurtDone()
        {
            baseEntity.EnemyData.IsHurt = false;
        }
    }
}
