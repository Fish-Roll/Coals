using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveByPoints : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform[] points;
    [SerializeField] private Animator animator;
    private int moveHash;
    private float _moveSpeed;
    private NavMeshAgent _agent;
    private Vector3 _target;
    private int _index = 0;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _moveSpeed = _agent.speed;
        moveHash = Animator.StringToHash("Move");
        isMoving = true;
        animator.SetBool(moveHash, true);
        UpdateDestination();
    }
    private void Update()
    {
        if (Vector3.Distance(_target, transform.position) < 0.5)
        {
            _index++;
            if (_index == points.Length)
                _index = 0;
            UpdateDestination();
        }
    }
    private void UpdateDestination()
    {
        _target = points[_index].position;
        _agent.SetDestination(_target);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.speed = 0;
            isMoving = false;
            animator.SetBool(moveHash, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            animator.SetBool(moveHash, true);
            _agent.speed = _moveSpeed;
        }
    }
}
