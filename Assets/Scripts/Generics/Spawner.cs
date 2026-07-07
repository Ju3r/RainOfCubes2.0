using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] private T _prefab;

    protected ObjectPool<T> _pool;

    private int _capacity = 5;

    public int SpawnedCount {  get; private set; }
    public int CreatedCount { get; private set; }
    public int ActiveCount => _pool.CountActive;

    public event Action Spawned;
    public event Action Created;
    public event Action RecalculatedActive;

    protected abstract void Subscription(T poolObject);
    protected abstract void Unsubscription(T poolObject);

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            Create,
            OnGet,
            OnRelease,
            OnDestroyObject,
            true,
            _capacity,
            _capacity);
    }

    protected virtual void Spawn()
    {
        _pool.Get();
    }

    protected virtual void OnGet(T poolObject)
    {
        SpawnedCount++;
        Spawned?.Invoke();
        RecalculatedActive?.Invoke();
        poolObject.Init();
        poolObject.Activate();
    }

    protected void OnRelease(T poolObject)
    {
        poolObject.Deactivate();
        RecalculatedActive?.Invoke();
        Unsubscription(poolObject);
    }

    private T Create()
    {
        T poolObject = Instantiate(_prefab);
        CreatedCount++;
        Created?.Invoke();
        RecalculatedActive?.Invoke();

        return poolObject;
    }

    private void OnDestroyObject(T poolObject)
    {
        Destroy(poolObject.gameObject);
    }
}
