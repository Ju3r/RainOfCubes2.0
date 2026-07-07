using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public abstract void Init();

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
