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


    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
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
        transform.localScale = new Vector3(scale.x, scale.y);
        var rectImage = RectTransform.rect;
        var width = rectImage.width;
        var height = rectImage.height;
        RectTransform.pivot = Vector2.zero;
        RectTransform.anchorMin = Vector2.zero;
        RectTransform.anchorMax = Vector2.zero;
        RectTransform.anchoredPosition = new Vector2(width * scale.x * indexI, height * scale.y * indexJ);
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