using UnityEngine;

namespace Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float damage;
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected float timeToLive;
        public abstract void Move();
    }
}