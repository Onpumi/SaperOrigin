using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour, ICellView, IPoolable<CellView>, IView
{
    [SerializeField] private Sprite[] _spriteNumbers;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Image _image;
    [SerializeField] private BrickView _brickView;
    [SerializeField] private FlagView _flagView;
    [SerializeField] private MineView _mineView;

    public InputHandler InputHandler { get; private set; }
    public BrickView BrickView => _brickView;
    public FlagView FlagView => _flagView;
    public MineView MineView => _mineView;

    private IDownAction _downAction;
    public CellData CellData { get; private set; }
    public FlagView FlagIcon { get; private set;  }
    private Sprite _emptySprite;

    private void Awake()
    {
        InputHandler = _inputHandler;
        _emptySprite = _image.sprite;
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

    public void ResetSprite()
    {
        _image.sprite = _emptySprite;
    }
    public void Init(CellData cellData)
    {
        cellData.Index1.TryThrowIfLessThanZero();
        cellData.Index2.TryThrowIfLessThanZero();
        CellData = cellData;
    }

    public bool InitFlag(bool value)
    {
        FlagView.InitFlag(value);
        return FlagView.Value;
    }

    public void SafeFlagIcon(FlagView flagIcon)
    {
        FlagIcon = flagIcon;
    }

    public void SpawnFrom(IPool<CellView> pool)
    {
        transform.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Destroy()
    {
        Destroy(transform);
    }
    
    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
}