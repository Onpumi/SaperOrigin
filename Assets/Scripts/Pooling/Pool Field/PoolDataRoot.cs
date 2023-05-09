using UnityEngine;


public class PoolDataRoot<T> where T : MonoBehaviour, IView, IPoolable<T>
{
    public readonly IPoolData<T> PoolData;
    private readonly int _size;
    private readonly Transform _parent;
    private readonly T _view;
    private readonly string _name;

    public PoolDataRoot(T view, Transform parent, int size, string name)
    {
        _parent = parent;
        _size = size;
        _view = view;
        _name = name;
        PoolData = new PoolDataBuilder<T>(_view, _parent, _size, _name);
    }
}