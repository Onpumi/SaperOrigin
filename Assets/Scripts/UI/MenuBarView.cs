using UnityEngine;

public class MenuBarView : WindowBase
{
    [SerializeField] private MenuType _menuType;
    [SerializeField] private RectTransform _rectTransformFieldCells;
    private float _anchorMinY;
    private Rect SafeArea => Screen.safeArea;
    public RectTransform RectTransform { get; private set; }
    public const float OffsetFromBottom = 100f;
    public float Height => RectTransform.rect.height;
    
    

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        if (_menuType == MenuType.Top)
        {
            SetPropertiesTopMenu();
        }
        else if (_menuType == MenuType.Bottom)
        {
            SetPropertiesBottomMenu();
        }
    }
    

    private void SetPropertiesTopMenu()
    {
        var anchorMin = SafeArea.position;
        var anchorMax = anchorMin + SafeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        _anchorMinY = anchorMax.y;
        RectTransform.anchorMin = new Vector2(anchorMin.x, _anchorMinY);
        RectTransform.anchorMax = anchorMax;
        RectTransform.anchoredPosition -= Vector2.up * (RectTransform.rect.height * 0.3f);
    }

    private void SetPropertiesBottomMenu()
    {
        RectTransform.pivot = Vector2.one * 0.5f;
        RectTransform.offsetMin = Vector2.zero;
        RectTransform.offsetMax = Vector2.zero;
        var anchorMin = new Vector2(0f, 0f);
        var anchorMax = new Vector2(1f, 0f);
        RectTransform.anchorMin = anchorMin;
        RectTransform.anchorMax = anchorMax;
        RectTransform.pivot = Vector2.zero;
        var heightMenu = 200f;
        RectTransform.sizeDelta = new Vector2(0f, heightMenu);
        var anchorPosition = RectTransform.anchoredPosition;
        RectTransform.anchoredPosition = new Vector2(anchorPosition.x, anchorPosition.y + OffsetFromBottom);
    }

    public void FitWidth(float width)
    {
        
        if (Screen.width > Screen.height)
        {
            var currentWidth = RectTransform.rect.width;
            var deltaOffset = ((currentWidth - width) * 0.5f) / currentWidth;
            RectTransform.anchorMin = new Vector2(deltaOffset, 0);
            RectTransform.anchorMax = new Vector2(1-deltaOffset, 0);
        }
    }

    public void FitSizeUI( RectTransform rectTransform, float scale = 1f )
    {
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.offsetMax = new Vector2(Height*0.5f * scale,0);
        rectTransform.offsetMin = new Vector2(-Height*0.5f * scale,0);;
    }
    
}

public enum MenuType
{
    Top,
    Bottom,
    Left,
    Right,
    Center
}