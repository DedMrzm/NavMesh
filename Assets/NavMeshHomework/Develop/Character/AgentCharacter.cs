using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamagable
{
    private NavMeshAgent _agent;

    private Health _health;
    private DirectionalRotator _rotator;

    private AgentJumper _jumper;

    [SerializeField] private CharacterView _characterView;

    [SerializeField] private float _maxHealth = 100;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _jumpSpeed;

    [SerializeField] private AnimationCurve _jumpCurve;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;

    public Vector3 CurrentPointToGo => _agent.destination;

    public bool InJumpProcess => _jumper.InProcess;

    public bool IsAlive { 
        get 
        {
            return _health.Value > 0;
        } 
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.acceleration = 999;
        _agent.updateRotation = false;

        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);
        _rotator = new DirectionalRotator(_rotationSpeed, transform);
        _health = new Health(_maxHealth);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);

        _currentHealth = _health.Value;
    }

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;

        _characterView.StartTakingDamage(_health.Value);
    }

    public void StopMove()
    {
        _agent.isStopped = true;
    }

    public void ResumeMove()
    {
        _agent.isStopped = false;
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData); 
        return false;
    }
    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
}