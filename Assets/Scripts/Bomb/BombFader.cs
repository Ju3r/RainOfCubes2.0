using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BombFader : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material _instanceMaterial;

    private float _fadeTime;
    private float _explosionTimeMin = 2;
    private float _explosionTimeMax = 5;

    private float _maxAlpha = 1;
    private float _minAlpha = 0;

    public event Action Faded;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _instanceMaterial = _meshRenderer.material;
    }

    public void StartFade()
    {
        _fadeTime = UnityEngine.Random.Range(_explosionTimeMin, _explosionTimeMax);
        StartCoroutine(Fading());
    }

    public void ResetMaterial()
    {
        Color color = Color.black;
        color.a = _maxAlpha;

        _instanceMaterial.color = color;
    }

    private IEnumerator Fading()
    {
        Color color = _instanceMaterial.color;
        float timer = 0;

        while (timer < _fadeTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(_maxAlpha, _minAlpha, timer / _fadeTime);
            _instanceMaterial.color = color;

            yield return null;
        }

        Faded?.Invoke();
    }
}