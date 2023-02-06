using UnityEngine;

namespace Assets.Interfaces
{
    public interface IEffectAfterDeath
    {
        public void InstantiateEffect(GameObject prefab, Vector3 position);
    }
}
