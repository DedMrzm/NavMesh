using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamagable
{
    private NavMeshAgent _agent;

    private Health _health;
    private ToPointAgentMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private GameObject _pointer;

    [SerializeField] private float _maxHealth = 100;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private CharacterView _characterView;

    [SerializeField] private LayerMask _walkableLayerMask;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Vector3 CurrentPointToGo => _mover.CurrentPoint;

    public bool IsAlive { 
        get 
        {
            return _health.Value > 0;
        } 
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new ToPointAgentMover(_agent, _moveSpeed, _walkableLayerMask, _pointer);
        _rotator = new DirectionalRotator(_rotationSpeed, transform);
        _health = new Health(_maxHealth);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);

        _currentHealth = _health.Value;
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;

        _characterView.CurrentState = CharacterView.States.TakeDamage;
    }

    public void StopMove()
    {
        _agent.isStopped = true;
    }

    public void ResumeMove()
    {
        _agent.isStopped = false;
    }

}