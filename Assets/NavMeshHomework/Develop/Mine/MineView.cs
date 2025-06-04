using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;

    [SerializeField] private GameObject _mineGameObject;

    public ParticleSystem ExplosionEffect => _explosionEffect;
    

    public void Explode()
    {
        _mineGameObject.SetActive(false);
        _explosionEffect.gameObject.SetActive(true);
        _explosionEffect.Play();
    }
}
