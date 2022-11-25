using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthBar;
        [SerializeField] private PlayerCharacteristics characteristics;
        private void Awake()
        {
            healthBar = GetComponent<Slider>();
            healthBar.maxValue = characteristics.MaxHealth;
            healthBar.value = characteristics.Health;
        }

        public void SetHealth(float health)
        {
            healthBar.value = health;
        }

        public void SetMaxHealth(float maxHealth)
        {
            healthBar.maxValue = maxHealth;
        }
    }
}
