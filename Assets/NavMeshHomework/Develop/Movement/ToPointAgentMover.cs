using UnityEngine;
using UnityEngine.AI;

public class ToPointAgentMover
{
    private const float ShowPointerValue = 1f;
    private readonly Vector3 PointerOffset = new Vector3(0, 0.15f, 0);

    private const KeyCode CastPointKey = KeyCode.Mouse0;

    private LayerMask _layerMask;

    private NavMeshAgent _agent;

    private bool _isGameStarted = false;

    private GameObject _pointerForCurrentPoint;

    public ToPointAgentMover(NavMeshAgent agent, float movementSpeed, LayerMask layerMask, GameObject pointer)
    {
        _pointerForCurrentPoint = pointer;
        _layerMask = layerMask; 
        _agent = agent;
        _agent.speed = movementSpeed;
        _agent.acceleration = 999;
    }

    public Vector3 CurrentVelocity => _agent.desiredVelocity;

    public Vector3 CurrentPoint;

    public void Update(float deltaTime)
    {
        if (Input.GetKeyDown(CastPointKey))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, _layerMask.value))
            {
                _pointerForCurrentPoint.transform.position = hit.point;

                _isGameStarted = true;
                CurrentPoint = hit.point;
            }
        }

        ShowOrHidePointer();
    }

    public void SetDestination(Vector3 position)
    {
        if (_isGameStarted)
        {
            _agent.SetDestination(position);
        }
    } 

    private void ShowOrHidePointer()
    {
        if(Vector3.Distance(_agent.transform.position, CurrentPoint) >= ShowPointerValue)
        {
            _pointerForCurrentPoint.SetActive(true);
        }
        else
        {
            _pointerForCurrentPoint.SetActive(false);
        }
    }
}
