using System;
using Player;
using UnityEngine;

namespace TestScripts
{
    public class TestKillScript : MonoBehaviour
    {
        public float hp;
        private PlayerCharacteristics _characteristics;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _characteristics = other.GetComponent<PlayerCharacteristics>();
                _characteristics.AddHealth(hp);
            }
        }
    }
}
