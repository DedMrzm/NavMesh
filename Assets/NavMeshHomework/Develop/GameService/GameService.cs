using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    private MMOController _characterController;

    private void Awake()
    {
        _characterController = new MMOController(_character);

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
