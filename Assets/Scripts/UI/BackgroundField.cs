using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundField : MonoBehaviour
{
    [SerializeField] private MenuBarView _topMenuBar;
    [SerializeField] private MenuBarView _bottomMenuBar;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rectParentTransform;
    private const float OffsetLeftRight = 0.05f;
    private float _offsetSpace;
    private GridLayoutGroup _gridLayoutGroup;
    
    public Rect Rect { get; private set; }

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void SetProperties()
    {
        
        
//        _rectTransform.anchorMin = Vector2.zero;
  //      _rectTransform.anchorMax = Vector2.one;
    //    _rectTransform.anchoredPosition = Vector2.zero;

    //transform.localScale = Vector2.zero;
    
/*
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
*/

        var topMenuRectTransform = _topMenuBar.RectTransform;
        var bottomMenuRectTransform = _bottomMenuBar.RectTransform;
        var heightBottomMenu = bottomMenuRectTransform.rect.height;
        var heightTopMenu = topMenuRectTransform.rect.height;


        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,_rectParentTransform.rect.width - 300f);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _rectParentTransform.rect.height - heightTopMenu - heightBottomMenu - Screen.safeArea.size.y );


/*        
    if (Screen.height > Screen.width)
        transform.localScale =
            new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
    else
        transform.localScale = new Vector3(_rectTransform.rect.height / _rectTransform.rect.width, 1f);
*/
        Rect = _rectTransform.rect;
        
    }


    public (int, int) InitGRID( float cellSize )
    {
        var cellSizeX = cellSize;
        var cellSizeY = cellSize;
        var rect = _rectTransform.rect;
        var width = rect.width;
        var height = rect.height;
        int countColumns = (int)(rect.width / cellSize);
        int countRows = (int)(rect.height / cellSize);
        Vector2 scalingFactor;
        scalingFactor.x = (width / countColumns) - cellSize;
        scalingFactor.y = (height / countRows) - cellSize;
        cellSizeX += scalingFactor.x;
        cellSizeY += scalingFactor.y;
        _gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
        _gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        _gridLayoutGroup.spacing = new Vector2(0, 0);
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = countColumns;
        _gridLayoutGroup.childAlignment = TextAnchor.LowerCenter;
        //Debug.Log(countColumns + " " + countRows);
        return (countColumns, countRows);
    }

    public void Init()
    {
        SetProperties();
        _borderField.Init(_rectTransform);
    }
}