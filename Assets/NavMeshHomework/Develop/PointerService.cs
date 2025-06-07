using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerService : MonoBehaviour
{
    private const float MinimumDistanceToHidePointer = 0.5f;

    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private AgentCharacter _character;

    private GameObject _currentPointer;

    public GameObject CurrentPointer => _currentPointer;

    private void Awake()
    {
        _currentPointer = Instantiate(_pointerPrefab);
        _currentPointer.SetActive(false);
    }

    private void Update()
    {
        if(_currentPointer != null)
        {
            if (Vector3.Distance(_character.CurrentPointToGo, _character.transform.position) > MinimumDistanceToHidePointer)
            {
                ShowPointer();
            }
            else
            {
                HidePointer();
            }
        }
    }

    private void ShowPointer()
    {
        _currentPointer.SetActive(true);
        _currentPointer.transform.position = _character.CurrentPointToGo;
    }

    private void HidePointer()
    {
        _currentPointer.SetActive(false);
    }

}
