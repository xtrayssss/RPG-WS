using Assets.Interfaces;
using UnityEngine;

namespace Assets.Character.Scripts
{
    class InjuredPlayer : MonoBehaviour, IDamagable
    {
        [SerializeField] PlayerData.PlayerData playerData;
        public void AcceptDamage(int damage)
        {
            playerData.CurrentHealth -= damage;
            Debug.Log(playerData.CurrentHealth);
        }
    }
}
