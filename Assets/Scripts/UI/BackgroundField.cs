using UnityEngine;
using UnityEngine.UI;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private WindowPanelView _windowPanel;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private float _offset = 0.05f;
    private const float OffsetTop = 0.02f;
    private RectTransform _rectTransform;
    public Rect Rect { get; private set; }

    private void Start()
    {
        SetProperties();
    }

    private void SetProperties()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchorMin = Vector2.zero + new Vector2( _offset,_offset);
        _rectTransform.anchorMax = Vector2.one - new Vector2( _offset, OffsetTop);
        _rectTransform.offsetMax = new Vector2(0f, -_windowPanel.RectTransform.rect.height-_borderField.HeightImage);
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
