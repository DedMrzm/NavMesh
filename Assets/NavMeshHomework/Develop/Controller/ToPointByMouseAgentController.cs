using UnityEngine;
using UnityEngine.AI;

public class ToPointByMouseAgentController : Controller
{
    private const KeyCode CastPointKey = KeyCode.Mouse0;

    private LayerMask _layerMask;

    private AgentCharacter _character;

    private Vector3 _currentPointToGo;

    public ToPointByMouseAgentController(AgentCharacter character, LayerMask layerMask)
    {
        _layerMask = layerMask; 
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _character.SetRotationDirection(_character.CurrentVelocity);
        if (Input.GetKeyDown(CastPointKey))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, _layerMask.value))
            {
                _currentPointToGo = hit.point;
                _character.SetDestination(_currentPointToGo);
            }
        }
    }
}
