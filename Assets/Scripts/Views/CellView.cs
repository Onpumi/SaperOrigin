using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour, ICellView, IPoolable<CellView>, IView
{
    [SerializeField] private Sprite[] _spriteNumbers;
    [SerializeField] private InputHandler _inputHandler;
    private Image _image;
    public InputHandler InputHandler { get; private set; }
    public BrickView BrickView { get; private set; }
    public MineView MineView { get; private set; }
    public FlagView FlagView { get; private set; }
    private IDownAction _downAction;
    public CellData CellData { get; private set; }
    public RectTransform RectTransform { get; private set; }
    private RectTransform _parentRectTransform;

    private Vector2 _fixedPosition;
    private Vector2 _sizeDelta = Vector2.zero;

    private int _indexI;
    private int _indexJ;
    private Vector2 _scale;


    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        //_sizeDelta = new Vector2(_parentRectTransform.rect.width, _parentRectTransform.rect.height);
        InputHandler = _inputHandler;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        BrickView ??= GetComponentInChildren<BrickView>();
    }

    public void InitAction(FieldCells field, IDownAction downAction)
    {
        _downAction = downAction ?? throw new ArgumentNullException("Selection need is not be null");
        _downAction.Select(field.Cells[CellData.Index1, CellData.Index2]);
    }

    public void SetTextNumbers(int value)
    {
        if (value >= 1 && value <= 8)
        {
            _image.sprite = _spriteNumbers[value - 1];
        }
    }


    public void Init(GameField gameField, IViews views, CellData cellData)
    {
        cellData.Index1.TryThrowIfLessThanZero();
        cellData.Index2.TryThrowIfLessThanZero();
        CellData = cellData;
        var parent = transform;
        
        FactoryViewPool<BrickView> factoryBrickView =
            new FactoryViewPool<BrickView>(gameField.PoolDataContainer.RootBricks.PoolData.Pool, parent);
        BrickView = factoryBrickView.Create();
        if (BrickView.transform.parent != parent)
            BrickView.transform.SetParent(parent);
        BrickView.transform.localPosition = Vector2.zero;
        FactoryViewPool<MineView> factoryMineView =
            new FactoryViewPool<MineView>(gameField.PoolDataContainer.RootMines.PoolData.Pool, parent);
        MineView = factoryMineView.Create();

        if (MineView.transform.parent != parent)
            MineView.transform.SetParent(parent);

        FactoryViewPool<FlagView> factoryFlagView =
            new FactoryViewPool<FlagView>(gameField.PoolDataContainer.RootFlags.PoolData.Pool, parent);

        FlagView = factoryFlagView.Create();

        if (FlagView.transform.parent != parent)
            FlagView.transform.SetParent(parent);

        FlagView.InitFlag(false);
        
    }


    public bool InitFlag(bool value)
    {
        FlagView.InitFlag(value);
        return FlagView.Value;
    }

    public void Display(int indexI, int indexJ, Vector2 scale)
    {
        _indexI = indexI;
        _indexJ = indexJ;
        _scale = scale;
        return;        
        transform.localScale = new Vector3(scale.x, scale.y);
        var rectParent = transform.parent.GetComponent<RectTransform>().rect;
        var rectImage = RectTransform.rect;
        var width = rectImage.width;
        var height = rectImage.height;
        RectTransform.pivot = Vector2.zero;
        //RectTransform.anchorMin = Vector2.one * 0.3f;
        //RectTransform.anchorMax = Vector2.one * 0.3f;
        //RectTransform.anchoredPosition = new Vector2(width * scale.x * indexI, height * scale.y * indexJ);
        var xAnchor = (scale.x / rectParent.width) * indexI * 100f;
        var yAnchor = (scale.y / rectParent.height) * indexJ * 100f;
        RectTransform.anchorMin = new Vector2(xAnchor,yAnchor);
        RectTransform.anchorMax = new Vector2(xAnchor,yAnchor);
        
        RectTransform.anchoredPosition = Vector2.zero;

        //Debug.Log( (scale.x / rectParent.width) * indexI * 100f ); 
        //_fixedPosition = transform.position;
        
    }

    private void OnRectTransformDimensionsChange()
    {
      //  Display(_indexI, _indexJ , _scale );
    }

    public void SpawnFrom(IPool<CellView> pool)
    {
        transform.gameObject.SetActive(true);
    }

    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }

    public float GetWidth() => GetComponent<Image>().sprite.rect.width;

    public float GetHeight() => GetComponent<Image>().sprite.rect.height;

    public Transform GetTransform() => transform;
}