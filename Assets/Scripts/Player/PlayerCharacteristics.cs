using UnityEngine;

namespace Player
{
    public class PlayerCharacteristics :MonoBehaviour
    {
        [field: SerializeField]public float MaxHealth { get; private set; }
        [field: SerializeField]public float Health { get; private set; }
        private PlayerStateMachine _ctx;
        private void Awake()
        {
            _ctx = GetComponent<PlayerStateMachine>();
        }
        public void AddMaxHealth(float add)
        {
            if (add < 0 && Mathf.Abs(add) > MaxHealth){
                MaxHealth = 0;
                return;
            }
            MaxHealth += add;
        }
        
        public void Heal(float add)
        {
            float newHp = Health + add;
            if (newHp > MaxHealth)
                Health = MaxHealth;
            else
                Health = newHp;
        }

        public void Damage(float dmg)
        {
            float newHp = Health - dmg;
            if (newHp < 0)
                Health = 0;
            else
                Health = newHp;
        }
    }
}