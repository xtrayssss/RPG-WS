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
        [SerializeField] private float totalDelaySeconds;
        [SerializeField] private bool isHurt;
        [SerializeField] private float timerTotalCoolDownAttack = 0.5f;
        private int currentHealth;
        
        public float TimerCoolDownAttack { get; set; }
        public float SpeedMove { get => speedMove; private set => speedMove = value; }
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int Damage { get => damage; private set => damage = value; }
        public int MaxHealth { get => maxHealth; private set => maxHealth = value; }
        public float TotalDelaySeconds { get => totalDelaySeconds; private set => totalDelaySeconds = value; }
        public bool IsHurt { get => isHurt; set => isHurt = value; }
        public float TimerTotalCoolDownAttack { get => timerTotalCoolDownAttack; private set => timerTotalCoolDownAttack = value; }
    }
}

