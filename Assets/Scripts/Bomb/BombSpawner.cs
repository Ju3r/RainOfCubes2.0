using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void CreateBomb(Vector3 position)
    {
        Bomb bomb = _pool.Get();
        Subscription(bomb);
        bomb.transform.position = position;
        bomb.StartExploding();
    }

    protected override void Subscription(Bomb bomb)
    {
        bomb.Exploded += ReleaseInPool;
    }

    protected override void Unsubscription(Bomb bomb)
    {
        bomb.Exploded -= ReleaseInPool;
    }

    private void ReleaseInPool(Bomb bomb)
    {
        _pool.Release(bomb);
    }
}