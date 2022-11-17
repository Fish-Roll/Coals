using Player;
using UnityEngine;

namespace InteractWithWorld
{
    public class HealItem : InteractableObject
    {
        [SerializeField] private float health;
        private PlayerCharacteristics _characteristics;
        public override void Interact()
        {
            _characteristics.Heal(health);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _characteristics = other.GetComponent<PlayerCharacteristics>();
        }
    }
}
