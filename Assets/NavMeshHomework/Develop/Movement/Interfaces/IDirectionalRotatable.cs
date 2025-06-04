using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectionalRotatable
{
    Quaternion CurrentRotation { get; }

    void SetRotationDirection(Vector3 inputDirection);
}
