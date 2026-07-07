using System;
using UnityEngine;

public class BombExploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5;
    [SerializeField] private float _force = 500;

    public event Action Exploded;

    public void Explode()
    {
        Collider[] objectsInCollider = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in objectsInCollider)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_force, transform.position, _explosionRadius);
            }
        }

        Exploded?.Invoke();
    }
}
