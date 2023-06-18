using System;
using UnityEngine;
using UnityEngine.UI;

//public class BrickView : MonoBehaviour, IBrickView, IPoolable<BrickView>
public class BrickView : MonoBehaviour, IBrickView
{
    [SerializeField] private RectTransform _rectTransform;
    public RectTransform RectTransform => _rectTransform;
    private void OnEnable() => transform.gameObject.SetActive(true);
    private void OnDisable() => transform.gameObject.SetActive(false);
    public void SetActive( bool value ) => transform.gameObject.SetActive(value);
    
/*
    public void SpawnFrom( IPool<BrickView> pool )
    {
        transform.gameObject.SetActive(true);
    }
        
    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
  */  
}
