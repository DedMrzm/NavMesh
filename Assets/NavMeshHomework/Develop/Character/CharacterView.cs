using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private const float MinimumMoveSpeedValue = 0.05f;

    private const float InjuredLayerValue = 30;
    private const int InjuredLayerIndex = 1;

    private const string IsRunningKey = "IsRunning";
    private const string HealthKey = "Health";
    private const string TakeDamageKey = "TakeDamage";

    [SerializeField] private AgentCharacter _character;
    [SerializeField] private Animator _animator;

    public States CurrentState = States.Idle;

    public enum States
    {
        Idle,
        Move,
        TakeDamage,
        Dead
    }

    private void Awake()
    {
        _animator.SetFloat(HealthKey, _character.MaxHealth);
    }

    private void Update()
    {
        ControlAnimations();

        if (_character.IsAlive == false)
        {
            CurrentState = States.Dead;
            return;
        }

        if(_character.CurrentVelocity.magnitude > MinimumMoveSpeedValue)
        {
            CurrentState = States.Move;
        }
        else
        {
            CurrentState = States.Idle;
        }
    }

    private void ControlAnimations()
    {
        switch(CurrentState)
        {
            case States.Idle:
                _animator.SetBool(IsRunningKey, false);
                break;

            case States.Move:
                _animator.SetBool(IsRunningKey, true);
                break;

            case States.TakeDamage:
                AnimateTakeDamage();
                break;

            case States.Dead:
                _animator.SetFloat(HealthKey, 0f);
                break;
        }
    }

    private void SetAnimationLayer(int indexOfLayer)
        => _animator.SetLayerWeight(indexOfLayer, 1f);

    private void AnimateTakeDamage()
    {
        _animator.SetFloat(HealthKey, _character.CurrentHealth);
        _animator.SetTrigger(TakeDamageKey);

        if (_character.CurrentHealth / _character.MaxHealth * 100 < InjuredLayerValue)
        {
            SetAnimationLayer(InjuredLayerIndex);
        }
    }
}
