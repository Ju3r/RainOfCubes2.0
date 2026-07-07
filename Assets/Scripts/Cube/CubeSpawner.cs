using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Vector3 _startAreaPosition;
    [SerializeField] private Vector3 _endAreaPosition;
    [SerializeField] private BombSpawner _bombSpawner;

    private float _delay = 1f;
    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);
        ModifyOnGet(cube);

        CubeDestroyer destroyer = cube.GetDestroyer();

        if (destroyer != null)
        {
            Subscription(cube);
        }
    }

    protected override void Subscription(Cube cube)
    {
        cube.GetDestroyer().NeedBomb += CreateBomb;
        cube.GetDestroyer().Destroyed += ReleaseInPool;
    }

    protected override void Unsubscription(Cube cube)
    {
        cube.GetDestroyer().NeedBomb -= CreateBomb;
        cube.GetDestroyer().Destroyed -= ReleaseInPool;
    }

    private void CreateBomb(Vector3 position)
    {
        _bombSpawner.CreateBomb(position);
    }

    private IEnumerator Spawning() {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isSpawning)
        {
            _pool.Get();
            yield return wait;
        }
    }

    private void ReleaseInPool(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ModifyOnGet(Cube cube)
    {
        cube.SetPosition(GetRandomPosition());
        cube.SetRotationToZero();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3();

        randomPosition.x = Random.Range(_startAreaPosition.x, _endAreaPosition.x);
        randomPosition.y = Random.Range(_startAreaPosition.y, _endAreaPosition.y);
        randomPosition.z = Random.Range(_startAreaPosition.z, _endAreaPosition.z);

        return randomPosition;
    }
}