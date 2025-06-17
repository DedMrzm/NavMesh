using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioService : MonoBehaviour
{
    private const KeyCode ToggleMusicKey = KeyCode.Alpha1;
    private const KeyCode ToggleSoundsKey = KeyCode.Alpha2;

    [SerializeField] private AudioMixer _audioMixer;

    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(ToggleMusicKey))
            ToggleMusic();

        if(Input.GetKeyDown(ToggleSoundsKey))
            ToggleSounds();
    }

    private void ToggleMusic()
    {
        if(_audioHandler.IsMusicOn())
            _audioHandler.OffMusic();
        else
            _audioHandler.OnMusic();
    }

    private void ToggleSounds()
    {
        if (_audioHandler.IsSoundOn())
            _audioHandler.OffSounds();
        else
            _audioHandler.OnSounds();
    }
}
