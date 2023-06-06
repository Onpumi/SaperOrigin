using UnityEngine;
using UnityEngine.UI;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private WindowPanelView _windowPanel;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private float _offset = 0.05f;
    private RectTransform _rectTransform;
    public Rect Rect { get; private set; }

    private void Start()
    {
        SetProperties();
    }

    private void SetProperties()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchorMin = new Vector2(_offset, _offset );
        _rectTransform.anchorMax = new Vector2(1f - _offset, 1f - 0.012f );
        _rectTransform.offsetMax = new Vector2(0f, -_windowPanel.RectTransform.rect.height);
        _rectTransform.offsetMin = new Vector2(0f, 0f);
        transform.localScale = new Vector3(1f, 1f); // вот так поле будет прямоугольным 
        // transform.localScale = new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
        Rect = _rectTransform.rect;
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init( _rectTransform );
    }
}
