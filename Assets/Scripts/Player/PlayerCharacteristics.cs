using UnityEngine;

namespace Player
{
    public class PlayerCharacteristics :MonoBehaviour
    {
        [field: SerializeField]public float MaxHealth
        {
            get;
            private set;
        }
        [field: SerializeField]public float Health
        {
            get;
            private set;
        }

        public PlayerCharacteristics(float maxHealth, float health)
        {
            MaxHealth = maxHealth;
            Health = health;
        }

        public void AddMaxHealth(float add)
        {
            if (add < 0 && Mathf.Abs(add) > MaxHealth){
                MaxHealth = 0;
                return;
            }
            MaxHealth += add;
        }

        public void AddHealth(float add)
        {
            if (add < 0 && Mathf.Abs(add) > Health)
                Health = 0;
            else if (add + Health > MaxHealth)
                Health = MaxHealth;
            else
                Health += add;
        }
    }
}