using UnityEngine;

namespace AI
{
    public class RotateHeadToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform head;
        [SerializeField] private float turnSpeed;
        private Quaternion _rotGoal;
        private Vector3 _direction;
        void Update()
        {
            _direction = (head.position - player.position).normalized;
            _rotGoal = Quaternion.LookRotation(_direction);
            head.rotation = Quaternion.Slerp(transform.rotation, _rotGoal, turnSpeed);
        }
    }
}
