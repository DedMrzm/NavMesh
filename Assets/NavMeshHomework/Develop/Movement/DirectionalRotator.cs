using UnityEngine;

public class DirectionalRotator
{
    private const float AccessToTurnValue = 0.05f;

    private float _rotationSpeed;

    private Vector3 _currentDirection;

    private Transform _transform;
    public DirectionalRotator(float rotationSpeed, Transform transform)
    {
        _rotationSpeed = rotationSpeed;
        _transform = transform;
    }

    public Quaternion CurrentRotation;

    public void SetInputDirection(Vector3 direction)
        => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude < AccessToTurnValue)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _rotationSpeed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }

}
