using UnityEngine;

namespace Player
{
    public class PlayerCharacteristics :MonoBehaviour
    {
        [field: SerializeField]public float MaxHealth { get; private set; }
        [field: SerializeField]public float Health { get; private set; }
        public void AddMaxHealth(float add) => MaxHealth += add;
        public void Heal(float add)
        {
            Health += add;
            Health = Mathf.Clamp(Health, 0f, MaxHealth);
        }

        public void Damage(float dmg)
        {
            Health -= dmg;
            Health = Mathf.Clamp(Health, 0f, MaxHealth);
        }
    }
}