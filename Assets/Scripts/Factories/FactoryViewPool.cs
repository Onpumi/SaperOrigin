using UnityEngine;
public class FactoryViewPool<T> : IFactoryView<T> where T : IPoolable<T>
{
    private Transform _parent;
    private Pool<T> _pool;

    public FactoryViewPool( Pool<T> pool, Transform parent ) 
    {
        _parent = parent;
        _pool = pool;
    }
    public T Create()
    {
        return _pool.Get();
    }
}