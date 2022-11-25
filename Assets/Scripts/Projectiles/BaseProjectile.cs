using UnityEngine;

namespace Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float damage;
        [SerializeField] protected GameObject projectile;
        public abstract void Move();
    }
}