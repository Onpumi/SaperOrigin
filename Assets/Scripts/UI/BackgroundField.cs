using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundField : MonoBehaviour
{
    [SerializeField] private MenuBarView _topMenuBar;
    [SerializeField] private MenuBarView _bottomMenuBar;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private RectTransform _rectTransform;
    private const float OffsetLeftRight = 0.05f;
    private float _offsetSpace;
    
    public Rect Rect { get; private set; }

    private void SetProperties()
    {
        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _rectTransform.anchoredPosition = Vector2.zero;
   //     _rectTransform.anchorMin = new Vector2(0.1f, 0.1f);
//        _rectTransform.anchorMax = new Vector2(0.9f, 0.9f);
        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        transform.localScale = new Vector3(1f, 1f); 

        var topMenuRectTransform = _topMenuBar.RectTransform;
        var bottomMenuRectTransform = _bottomMenuBar.RectTransform;
        var heightBottomMenu = bottomMenuRectTransform.rect.height;
        var heightTopMenu = topMenuRectTransform.rect.height;
        var positionBottomRect = bottomMenuRectTransform.anchoredPosition;
        var offsetTop = heightTopMenu + _borderField.HeightImage * 2f;
        _offsetSpace = 30f;
        var offsetBottom = heightBottomMenu + _borderField.HeightImage +
                           positionBottomRect.y + _offsetSpace;
        _rectTransform.offsetMax = new Vector2(-_borderField.WidthImage-50f, -offsetTop);
        _rectTransform.offsetMin = new Vector2(_borderField.WidthImage+50f, offsetBottom);

         
/*        
    if (Screen.height > Screen.width)
        transform.localScale =
            new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
    else
        transform.localScale = new Vector3(_rectTransform.rect.height / _rectTransform.rect.width, 1f);
*/
        Rect = _rectTransform.rect;
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init(_rectTransform);
    }
}