using UnityEngine;

public class PoolDataBuilder<T> : IPoolData<T> where T : MonoBehaviour, IView, IPoolable<T>
{
    public Pool<T> Pool { get; private set; }
    public IPoolFactory<T> FactoryCellViewPool { get; private set; }

    public PoolDataBuilder( T ObjectView, Transform parent, int sizePool, string name )
    {
        FactoryCellViewPool ??= new PrefabFactory<T>(ObjectView, parent, name);
        Pool ??= new Pool<T>(FactoryCellViewPool, sizePool);
    }
}

