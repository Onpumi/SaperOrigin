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


