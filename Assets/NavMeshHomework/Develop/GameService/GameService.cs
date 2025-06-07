using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _walkableArea;

    private Controller _characterController;

    private void Awake()
    {
        _characterController = new ToPointByMouseAgentController(_character, _walkableArea);

        _characterController.Enable();
    }

    private void Update()
    {
        if (_character.IsAlive)
        {
            _characterController.Update(Time.deltaTime);
        }
    }
}
