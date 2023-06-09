using UnityEngine;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private MenuBarView _topMenuBar;
    [SerializeField] private MenuBarView _bottomMenuBar;
    [SerializeField] private BorderField _borderField;
    private const float OffsetLeftRight = 0.05f;
    private const float OffsetTopBottom = 300f;
    private RectTransform _rectTransform;
    public Rect Rect { get; private set; }

    private void Start()
    {
        SetProperties();
    }

    private void SetProperties()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchorMin = Vector2.zero + new Vector2(OffsetLeftRight, 0);
        _rectTransform.anchorMax = Vector2.one - new Vector2(OffsetLeftRight, 0);
        var offsetTop = _topMenuBar.RectTransform.rect.height + _borderField.HeightImage * 2f;
        var offsetBottom = _bottomMenuBar.RectTransform.rect.height + _borderField.HeightImage * 2f;
        _rectTransform.offsetMax = new Vector2(0f, -offsetTop);
        _rectTransform.offsetMin = new Vector2(0f, offsetBottom);
            //  transform.localScale = new Vector3(1f, 1f); // вот так поле будет прямоугольным
        
        if (Screen.height > Screen.width)
            transform.localScale =
                new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
        else
            transform.localScale = new Vector3(_rectTransform.rect.height / _rectTransform.rect.width, 1f);

          transform.localScale = new Vector3(1f, 1f); // вот так поле будет прямоугольным
          
        Rect = _rectTransform.rect;
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init(_rectTransform);
    }
}