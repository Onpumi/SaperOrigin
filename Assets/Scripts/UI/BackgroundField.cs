using UnityEngine;
using UnityEngine.UI;


public class BackgroundField : MonoBehaviour
{
    [SerializeField] private MenuBarView _topMenuBar;
    [SerializeField] private MenuBarView _bottomMenuBar;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rectParentTransform;
    [SerializeField] private SectionMenuView _rightTopMenuView;
    [SerializeField] private SectionMenuView _leftTopMenuView;
    [SerializeField] private SectionMenuView _centerTopMenuView;
    
    public BorderField BorderField => _borderField;
    public RectTransform RectTransform => _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private float _cellSize;
    public float Width { get; private set; }

    public Rect Rect { get; private set; }

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void SetProperties(GameField gameField)
    {
        var topMenuRectTransform = _topMenuBar.RectTransform;
        var bottomMenuRectTransform = _bottomMenuBar.RectTransform;
        var heightBottomMenu = bottomMenuRectTransform.rect.height;
        var heightTopMenu = topMenuRectTransform.rect.height;
        var positionBottomRect = bottomMenuRectTransform.anchoredPosition;
        var positionTopRect = topMenuRectTransform.anchoredPosition;
        var offsetBottom = heightBottomMenu + _borderField.HeightImage +
                           positionBottomRect.y;
        var offsetTop = heightTopMenu + _borderField.HeightImage * 2f - positionTopRect.y;
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            _rectParentTransform.rect.width - 2f * (_borderField.WidthImage + 50f));
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            _rectParentTransform.rect.height - offsetTop - offsetBottom - 100f);
        var top = topMenuRectTransform.anchoredPosition.y;
        var bottom = bottomMenuRectTransform.anchoredPosition.y;
        var topSizeOffsetMenu = topMenuRectTransform.sizeDelta.y * 0.5f;
        var bottomSizeOffsetMenu = bottomMenuRectTransform.sizeDelta.y * 0.5f;
        var centerVerticalPosition = ((bottom - bottomSizeOffsetMenu) - (top - topSizeOffsetMenu)) * 0.5f;
        _rectTransform.anchoredPosition = new Vector2(_rectParentTransform.anchoredPosition.x, centerVerticalPosition);

/*        
    if (Screen.height > Screen.width)
        transform.localScale =
            new Vector3(1f, _rectTransform.rect.width / _rectTransform.rect.height); // так оно станет квадратным
    else
        transform.localScale = new Vector3(_rectTransform.rect.height / _rectTransform.rect.width, 1f);
*/
        Rect = _rectTransform.rect;
    }


    public (int, int) InitGRID(float cellSize)
    {
        _cellSize = cellSize;
        var cellSizeX = cellSize;
        var cellSizeY = cellSize;
        var rect = _rectTransform.rect;
        var width = rect.width;
        var height = rect.height;
        Width = width;
        int countColumns = (int)(rect.width / cellSize);
        int countRows = (int)(rect.height / cellSize);
        Vector2 scalingFactor;
        scalingFactor.x = (width / countColumns) - cellSize;
        scalingFactor.y = (height / countRows) - cellSize;
        cellSizeX += scalingFactor.x;
        cellSizeY += scalingFactor.y;
        _gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
        _gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
        _gridLayoutGroup.spacing = new Vector2(0, 0);
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = countColumns;
        _gridLayoutGroup.childAlignment = TextAnchor.LowerCenter;
        _gridLayoutGroup.startCorner = GridLayoutGroup.Corner.LowerRight;

        return (countColumns, countRows);
    }

    public (int, int) UpdatePropertiesInGrid(int countColumns)
    {
        _gridLayoutGroup.constraintCount = countColumns;
        if (Screen.width > Screen.height)
        {
            var widthParent = _rectTransform.rect.width;
            var differenceCountColumns = (int)(widthParent / _cellSize) - countColumns;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                _rectParentTransform.rect.width - 2f * (_borderField.WidthImage + 50f) -
                differenceCountColumns * _cellSize);
        }

        int countRows;
        (countColumns, countRows) = InitGRID(_cellSize);
        return (countColumns, countRows);
    }

    public void Init(GameField gameField)
    {
        SetProperties(gameField);
       // var rectTransformMenuBar = _topMenuBar.GetComponent<RectTransform>();
        //  if (Screen.width <= Screen.height)
        //    _borderField.Init(_rectTransform);
    }

    public void FitSizeMenu()
    {
        var rectTransformMenuBar = _topMenuBar.GetComponent<RectTransform>();
        var anchorYMin = rectTransformMenuBar.anchorMin.y;
        var anchorYMax = rectTransformMenuBar.anchorMax.y;
        rectTransformMenuBar.anchorMin = new Vector2(0.5f, anchorYMin );
        rectTransformMenuBar.anchorMax = new Vector2(0.5f, anchorYMax );
        rectTransformMenuBar.sizeDelta = new Vector2( _rectTransform.sizeDelta.x, rectTransformMenuBar.sizeDelta.y );
        var sizeCellForMenu = (rectTransformMenuBar.rect.width - _centerTopMenuView.GetWidth()) / 4f;
        _rightTopMenuView.InitCellSize(sizeCellForMenu);    
        _leftTopMenuView.InitCellSize(sizeCellForMenu);
    }
}