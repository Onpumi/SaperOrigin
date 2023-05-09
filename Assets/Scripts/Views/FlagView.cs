using System;
using UnityEngine;
using UnityEngine.UI;

public class FlagView : MonoBehaviour, IFlagView, IPoolable<FlagView>
{
    [SerializeField] private CellView _parent;
    [SerializeField] private Sprite _spriteError;
    private Image _image;
    public bool Value { get; private set; }

    private void Awake()
    {
        gameObject.SetActive(false);
        _image = GetComponent<Image>();
        Value = false;
    }

    private void OnEnable()
    {
        
        Value = true;
    }

    private void OnDisable()
    {
        Value = false;
    }

    public float GetWidth() => _image.sprite.rect.width;

    public float GetHeight() => _image.sprite.rect.height;

    public void SetFlagError()
    {
        _image.sprite = _spriteError;
        transform.localScale = Vector3.one;
    }

    public void SetParent( ICell cell )
    {
        transform.SetParent(cell.CellView.GetTransform());
    }

    public void Display()
    {
        transform.gameObject.SetActive(true);
        transform.SetAsFirstSibling();
    }

    public void SetActive(bool value) => transform.gameObject.SetActive(value);
    public void InitFlag( bool value )
    {
        transform.gameObject.SetActive(value);
        transform.localScale = Vector3.one / 1.5f;
    }
    
    public Transform GetTransform() => transform;
    
    
    public void SpawnFrom( IPool<FlagView> pool )
    {
        transform.gameObject.SetActive(true);
    }

        
    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
}
