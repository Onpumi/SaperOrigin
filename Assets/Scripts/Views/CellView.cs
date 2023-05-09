using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour, ICellView, IPoolable<CellView>, IView
{
    [SerializeField] private Sprite[] _spriteNumbers;
    [SerializeField] private InputHandler _inputHandler;
    public float WidthSpriteCell { get; private set; }
    public float HeightSpriteCell { get; private set; }
    public float PixelPerUnit { get; private set; }
    private Sprite _sprite;
    private Image _image;
    private Transform _canvasParent;
    public InputHandler InputHandler { get; private set; }
    public BrickView BrickView { get; private set; }
    public MineView MineView { get; private set; }
    public FlagView FlagView { get; private set; }
    private IDownAction _downAction;
    public CellData CellData { get; private set; }


    private void Awake()
    {
        _sprite = GetComponent<Image>().sprite ?? throw new ArgumentNullException("Sprite cell need is not null!");
        InputHandler = _inputHandler;
        WidthSpriteCell = _sprite.rect.width;
        HeightSpriteCell = _sprite.rect.height;
        
//        PixelPerUnit = transform.parent.GetComponent<CanvasScaler>().referencePixelsPerUnit;
        PixelPerUnit = transform.parent.parent.GetComponent<CanvasScaler>().referencePixelsPerUnit;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        BrickView ??= GetComponentInChildren<BrickView>();
    }

    public bool InitAction(FieldCells field, IDownAction downAction)
    {
        _downAction = downAction ?? throw new ArgumentNullException("Selection need is not be null");
        return _downAction.Select(field.Cells[CellData.Index1, CellData.Index2]);
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

    public void Display(ICell cell, Vector3 positionStart, float scale)
    {
        var deltaX = PixelPerUnit / WidthSpriteCell;
        var deltaY = PixelPerUnit / HeightSpriteCell;
        var widthSprite = WidthSpriteCell * scale * deltaX;
        var heightSprite = HeightSpriteCell * scale * deltaY;
        var camera = Camera.main;
        var currentPosition = new Vector3(positionStart.x, positionStart.y, 0f);
        var currentPositionScreen = camera.WorldToScreenPoint(currentPosition);
        currentPositionScreen.x += (float)widthSprite * (float)cell.CellData.Index1;
        currentPositionScreen.y += (float)heightSprite * (float)cell.CellData.Index2;
        var resultPosition = camera.ScreenToWorldPoint(currentPositionScreen);
        transform.position = resultPosition;
        transform.localScale = new Vector3(scale, scale, 0);
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