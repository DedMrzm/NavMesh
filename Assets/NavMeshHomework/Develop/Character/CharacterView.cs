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
    private const string InJumpProcessKey = "InJumpProcess";

    [SerializeField] private AgentCharacter _character;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Debug.Log("Set health");
        _animator.SetFloat(HealthKey, _character.MaxHealth);
    }

    private void Update()
    {

        if (_character.IsAlive == false)
        {
            return;
        }
        
        _animator.SetBool(InJumpProcessKey, _character.InJumpProcess);

        if (_character.CurrentVelocity.magnitude > MinimumMoveSpeedValue)
        {
            StartRunning();
        }
        else
        {
            StopRunning();
        }
    }
    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
    
    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    public void StartTakingDamage(float currentHealth)
    {
        _animator.SetFloat(HealthKey, currentHealth);
        _animator.SetTrigger(TakeDamageKey);

        if (currentHealth / _character.MaxHealth * 100 < InjuredLayerValue)
            SetAnimationLayer(InjuredLayerIndex);
    }

    private void SetAnimationLayer(int indexOfLayer)
        => _animator.SetLayerWeight(indexOfLayer, 1f);
}
