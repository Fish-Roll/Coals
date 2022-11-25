using System.Collections;
using UnityEngine;

namespace Projectiles
{
    public class BasicFireball:BaseProjectile
    {
        [SerializeField] private Rigidbody rb;
        private bool _beginDestroying; 
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _beginDestroying = false;
            StartCoroutine(nameof(DestroyTimer));
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            //if(!other.CompareTag("Player"))
                Destroy(gameObject);
        }

        private IEnumerator DestroyTimer()
        {
            if (_beginDestroying)
                yield break;
            Debug.Log("Begin timer for destroy");
            _beginDestroying = true;
            yield return new WaitForSeconds(timeToLive);
            Destroy(gameObject);
            _beginDestroying = false;
        }
        public override void Move()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            //rb.velocity = Vector3.forward * speed;
        }
    }
}