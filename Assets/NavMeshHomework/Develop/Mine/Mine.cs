using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _timeBeforeExplosion;
    [SerializeField] private float _damage;

    [SerializeField] MineView _mineView;

    private SphereCollider _collider;

    public bool IsTimerStarted = false;
    public bool IsStartExplode = false;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");

        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();

        if (other.gameObject.GetComponent<IDamagable>() != null)
        {
            StartCoroutine(TimerProcess());
        }
    }

    private void Explode()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _radius);    
        
        foreach(Collider target in targets)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if(damagable != null)
            {
                damagable.TakeDamage(_damage);
            }
        }

        Destroy(gameObject, _mineView.ExplosionEffect.main.duration);
    }

    private IEnumerator TimerProcess()
    {
        IsTimerStarted = true;

        yield return new WaitForSeconds(_timeBeforeExplosion);

        IsStartExplode = true;
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, _radius);
    }
}
