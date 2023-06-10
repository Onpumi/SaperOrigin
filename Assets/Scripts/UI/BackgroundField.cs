using UnityEngine;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private MenuBarView _topMenuBar;
    [SerializeField] private MenuBarView _bottomMenuBar;
    [SerializeField] private BorderField _borderField;
    private const float OffsetLeftRight = 0.05f;
    private float _offsetSpace;
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
        var topMenuRectTransform = _topMenuBar.RectTransform;
        var bottomMenuRectTransform = _bottomMenuBar.RectTransform;
        var heightBottomMenu = bottomMenuRectTransform.rect.height;
        var heightTopMenu = topMenuRectTransform.rect.height;
        var positionBottomRect = bottomMenuRectTransform.anchoredPosition;
        var offsetTop = heightTopMenu + _borderField.HeightImage * 2f;
        _offsetSpace = heightBottomMenu * 0.2f;
        var offsetBottom = heightBottomMenu + _borderField.HeightImage +
                           positionBottomRect.y + _offsetSpace;
        _rectTransform.offsetMax = new Vector2(0f, -offsetTop);
        _rectTransform.offsetMin = new Vector2(0f, offsetBottom);
        //  transform.localScale = new Vector3(1f, 1f); // вот так поле будет прямоугольным

        /*
    if (Screen.height > Screen.width)
        transform.localScale =
            new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
    else
        transform.localScale = new Vector3(_rectTransform.rect.height / _rectTransform.rect.width, 1f);
*/
        transform.localScale = new Vector3(1f, 1f); // вот так поле будет прямоугольным

        Rect = _rectTransform.rect;
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init(_rectTransform);
    }
}