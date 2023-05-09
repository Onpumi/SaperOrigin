using UnityEngine;


public class FactoryView<T> : IFactoryView<T> where T : MonoBehaviour
{
    private T _prefabView;
    private Transform _parent;

    public FactoryView( T prefabView, Transform parent) 
    {
        _prefabView = prefabView;
        _parent = parent;
    }
    public T Create()
    {
        T createdView;
        createdView = Object.Instantiate( _prefabView, _parent );
        createdView.transform.localScale = Vector3.one;
        return createdView;
    }
}



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

