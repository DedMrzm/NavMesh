using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private float _movementSpeed;

    private Vector3 _currentDirection;
    public DirectionalMover
        (CharacterController characterController, 
        float movementSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
    }
    
    public Vector3 CurrentVelocity;

    public void SetInputDirection(Vector3 direction)
        => _currentDirection = direction;


    private void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _movementSpeed;

        _characterController.Move(CurrentVelocity * deltaTime);
    }
}
