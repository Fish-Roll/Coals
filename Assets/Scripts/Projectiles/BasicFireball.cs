using Player;
using UnityEngine;

namespace Projectiles
{
    public class BasicFireball:BaseProjectile
    {
        [SerializeField] private Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Move();
        }
        public override void Move()
        {
            rb.velocity = Vector3.forward * speed;
        }
    }
}