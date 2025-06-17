using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Light _lightEffect;

    [SerializeField] private MeshRenderer _mineAppearance;
    [SerializeField] private Mine _mine;

    [SerializeField] private AudioClip _prepareToExplosionSound;
    [SerializeField] private AudioClip _explosionSound;

    [SerializeField] private AudioSource _audioSource;

    private bool _isPrepareToExplosionSoundPlaying = false;
    private bool _isExplosionSoundPlaying = false;

    public ParticleSystem ExplosionEffect => _explosionEffect;


    private void Update()
    {
        if (_mine.IsTimerStarted)
        {
            Debug.Log("Someone step on mine");
            ShowPrepareOfExplosion();
        }
        if (_mine.IsStartExplode)
        {
            Explode();
        }
    }

    private void Explode()
    {
        PlaySound(_explosionSound, ref _isExplosionSoundPlaying);

        _mineAppearance.gameObject.SetActive(false);
        _explosionEffect.gameObject.SetActive(true);
    }

    private void ShowPrepareOfExplosion()
    {
        PlaySound(_prepareToExplosionSound, ref _isPrepareToExplosionSoundPlaying);

        _lightEffect.gameObject.SetActive(true);
    }

    private void PlaySound(AudioClip clip, ref bool isPlaying)
    {
        if (isPlaying == false)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            isPlaying = true;
        }
    }
}
