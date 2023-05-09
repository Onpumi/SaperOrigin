using UnityEngine;

public interface IPoolData<T> where T : MonoBehaviour, IView, IPoolable<T>
{
    public Pool<T> Pool { get; }
}
