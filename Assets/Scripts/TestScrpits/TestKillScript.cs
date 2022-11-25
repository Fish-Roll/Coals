using System;
using Player;
using UnityEngine;

namespace TestScripts
{
    public class TestKillScript : MonoBehaviour
    {
        public float hp;
        private PlayerCharacteristics _characteristics;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _characteristics = other.GetComponent<PlayerCharacteristics>();
                _characteristics.Damage(hp);
            }
        }
    }
}
