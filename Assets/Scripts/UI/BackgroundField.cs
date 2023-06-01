using UnityEngine;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private WindowPanelView _windowPanel;
    [SerializeField] private BorderField _borderField;
    private RectTransform _rectTransform;
    public Rect Rect { get; private set; }

    private void Start()
    {
        SetProperties();;
    }

    private void SetProperties()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchorMin = new Vector2(0.05f, 0.05f);
        _rectTransform.anchorMax = new Vector2(0.95f, 0.95f);
        _rectTransform.offsetMax = new Vector2(0f, -_windowPanel.RectTransform.rect.height);
        _rectTransform.offsetMin = new Vector2(0f, 0f);
        transform.localScale = new Vector3(1f, 1f);
        Rect = _rectTransform.rect;
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init( _rectTransform );
    }
}
