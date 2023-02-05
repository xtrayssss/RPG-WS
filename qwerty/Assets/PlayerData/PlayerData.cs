using UnityEngine;

namespace Assets.Character.PlayerData
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Player/Data")]
    public class PlayerData : ScriptableObject
    {
        private void Awake()
        {
            currentHealth = MaxHealth;
        }

        [SerializeField] private float speedMove = 10.0f;
        [SerializeField] private int maxHealth = 50;
        [SerializeField] private int damage;
        private int currentHealth;
        public float SpeedMove { get => speedMove; private set => speedMove = value; }
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int Damage { get => damage; private set => damage = value; }
        public int MaxHealth { get => maxHealth; private set => maxHealth = value; }
    }
}

