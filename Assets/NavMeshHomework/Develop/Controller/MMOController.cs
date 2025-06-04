using UnityEngine;

public class MMOController : Controller
{
    private const KeyCode CastPointKey = KeyCode.Mouse0;
    
    private AgentCharacter _character;

    private LayerMask _layerMask;

    public MMOController(AgentCharacter character)
    {
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _character.SetDestination(_character.CurrentPointToGo);
        _character.SetRotationDirection(_character.CurrentVelocity);
    }
}
