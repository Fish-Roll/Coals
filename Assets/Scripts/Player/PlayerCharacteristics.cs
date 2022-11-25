using HUD;
using UnityEngine;

namespace Player
{
    public class PlayerCharacteristics :MonoBehaviour
    {
        [field: SerializeField]public float MaxHealth { get; private set; }
        [field: SerializeField]public float Health { get; private set; }
        [SerializeField] private HealthBar healthBar;

        public void AddMaxHealth(float add)
        {
            MaxHealth += add;
            MaxHealth = Mathf.Clamp(MaxHealth, 0f, MaxHealth);
            healthBar.SetMaxHealth(MaxHealth);
        }

        public void Heal(float add)
        {
            Health += add;
            Health = Mathf.Clamp(Health, 0f, MaxHealth); 
            healthBar.SetHealth(Health);
        }

        public void Damage(float dmg)
        {
            Health -= dmg;
            Health = Mathf.Clamp(Health, 0f, MaxHealth);
            healthBar.SetHealth(Health);
        }
    }
}