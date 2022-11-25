using System.Collections;
using UnityEngine;

namespace Player
{
    public class BaseAttack : MonoBehaviour
    {
        [SerializeField] private Transform projectile;
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
            yield return new WaitForSeconds(0.5f);
            _countProjectiles++;
            _isRunning = false;
        }
    }
}