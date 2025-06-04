using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _radius;

    [SerializeField] private float _timeBeforeExplosion;

    [SerializeField] private float _damage;

    private SphereCollider _collider;

    private bool _isStartExplode;
    private float _timer;

    private List<IDamagable> _damagablesObjects = new List<IDamagable>();

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;
    }

    private void Update()
    {
        if (_isStartExplode)
        {
            _timer += Time.deltaTime;
            if(_timer > _timeBeforeExplosion)
            {
                Explode();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");

        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();

        if (other.gameObject.GetComponent<IDamagable>() != null)
        {
            Debug.Log("ENTER DAMAGABLE");
            _damagablesObjects.Add(damagable);
            _isStartExplode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IDamagable>() != null)
        {
            _damagablesObjects.Remove(other.gameObject.GetComponent<IDamagable>());
        }
    }

    private void Explode()
    {
        foreach(IDamagable damagableObject in _damagablesObjects)
        {
            damagableObject.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        
    }
}
