using System.Collections;
using UnityEngine;

namespace Attack
{
    public class BaseAttack : MonoBehaviour
    {
        [SerializeField] private Transform projectile;
        [SerializeField] private float timeAttack;
        private bool _isRunning = false;
        private int _countProjectiles = 0;
        public void Attack(Vector3 mousePosition, Transform[] spawnPositions)
        {
            StartCoroutine(AttackMethod(mousePosition, spawnPositions));
        }
        private IEnumerator AttackMethod(Vector3 mousePosition, Transform[] spawnPositions)
        {
            if (_isRunning)
                yield break;
            if (_countProjectiles >= spawnPositions.Length)
                _countProjectiles = 0;
            _isRunning = true;
            Vector3 rotate = (mousePosition - spawnPositions[_countProjectiles].position).normalized;
            Instantiate(projectile, spawnPositions[_countProjectiles].position, Quaternion.LookRotation(rotate, Vector3.up));
            yield return new WaitForSeconds(timeAttack);
            _countProjectiles++;
            _isRunning = false;
        }
    }
}