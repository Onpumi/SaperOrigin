using System;
using UnityEngine;
using UnityEngine.UI;

public class BrickView : MonoBehaviour, IBrickView, IPoolable<BrickView>
{
    [SerializeField] private CellView _cellView;
    private void OnEnable() => transform.gameObject.SetActive(true);
    private void OnDisable() => transform.gameObject.SetActive(false);
    public float GetWidth() => GetComponent<Image>().sprite.rect.width;
    public float GetHeight() => GetComponent<Image>().sprite.rect.height;
    public void SetActive( bool value ) => transform.gameObject.SetActive(value);
    public Transform GetTransform() => transform;


    public void SpawnFrom( IPool<BrickView> pool )
    {
        transform.gameObject.SetActive(true);

    }

        
    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
    
}
