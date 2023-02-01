using UnityEngine;

namespace Assets.Character.PlayerData
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Player/Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float speedMove = 10.0f;
        [SerializeField] private int health = 50;
        public float SpeedMove { get => speedMove; private set => speedMove = value; }
        public int Health { get => health; set => health = value; }
    }
}

