using System;
using UnityEngine;

[RequireComponent(typeof(BombFader), typeof(BombExploder), typeof(Rigidbody))]
public class Bomb : PoolObject
{
    private BombFader _fader;
    private BombExploder _exploder;
    private Rigidbody _rigidbody;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _fader = GetComponent<BombFader>();
        _exploder = GetComponent<BombExploder>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _fader.Faded += Explode;
        _exploder.Exploded += OnExploded;
    }

    private void OnDisable()
    {
        _fader.Faded -= Explode;
        _exploder.Exploded -= OnExploded;
    }

    public override void Init()
    {
        _fader.ResetMaterial();
    
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void StartExploding()
    {
        _fader.StartFade();
    }

    private void Explode()
    {
        _exploder.Explode();
    }

    private void OnExploded()
    {
        Exploded?.Invoke(this);
    }
}
