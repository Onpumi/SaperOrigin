using System;
using UnityEngine;
using UnityEngine.UI;

public class GameField : WindowBase, IGameField, IBackToPreviousWindowCommand
{
    [SerializeField] private UIData _uiData;
    [SerializeField] private Views _views;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sounds _sounds;
    [SerializeField] private IWindowCommand _backWindowCommand;
    [SerializeField] private Transform _parentField;
    public DataSetting DataSetting { get; private set; }
    public UIData UIData => _uiData;
    public Views Views => _views;
    public ScreenAdjusment ScreenAdjusment { get; private set; }
    public SpriteData SpriteData { get; private set; }
    public GameState GameState => _gameState;
    public Sounds Sounds => _sounds;
    public Camera CameraField => Camera.main;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;
    public PoolDataContainer PoolDataContainer { get; private set; }


    private void Init()
    {
        ScreenAdjusment = new ScreenAdjusment(transform);
        var width = _views.CellView.GetWidth();
        var height = _views.CellView.GetHeight();
        SpriteData = new SpriteData(width, height);
        DataSetting = new DataSetting(this);
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        SetPercentMine((TypesGame)DataSetting.GameData.GetDifficultValue());
        var parent = _parentField;
        PoolDataContainer = new PoolDataContainer(_views, parent, 1000);
    }

    public override void Enable()
    {
    }

    public void SetPercentMine(TypesGame typesGame)
    {
        switch (typesGame)
        {
            case TypesGame.DifficultGame:
                _gameState.GameFieldData.PercentMine = 20;
                break;
            case TypesGame.MediumGame:
                _gameState.GameFieldData.PercentMine = 15;
                break;
            case TypesGame.EasyGame:
                _gameState.GameFieldData.PercentMine = 10;
                break;
            default:
                throw new ArgumentException("TypesGame is wrong value!");
        }
    }

    private void Start()
    {
        Init();
        new FieldCells(this);
    }

    public float CalculateScale()
    {
        var image = _parentField.GetComponent<Image>();
        var scaleBrick = 1f;
        var rectImage = image.rectTransform.rect;
        var screenArea = rectImage.width * rectImage.height; 
        var spriteArea = Mathf.Pow(ScreenAdjusment.PixelsPerUnit, 2);
        var deltaScale = Mathf.Sqrt(screenArea / (_gameState.GameFieldData.NeedCountBricks * spriteArea));
        scaleBrick *= deltaScale;
        return scaleBrick;
    }


    public float GetScaleDefault()
    {
        var image = _parentField.GetComponent<Image>();
        var scaleBrick = 1f;
        var needProcentDeltaWidth = 10;
        return scaleBrick;
    }

    public Vector2 GetSizePerUnit()
    {
        var scaleX = _gameState.GameFieldData.ScaleBrick;
        var scaleY = _gameState.GameFieldData.ScaleBrick / _gameState.GameFieldData.ScaleHeightGrid;
        var resolutionCanvas = ScreenAdjusment.ResolutionCanvas;
        var refPixelsPerUnit = ScreenAdjusment.RefPixelsPerUnit;
        return new Vector2(resolutionCanvas.x / (refPixelsPerUnit * scaleX),
            resolutionCanvas.y / (refPixelsPerUnit * scaleY));
    }

    public Vector2Int GetCountBlocksXY()
    {
        var scale = _gameState.GameFieldData.ScaleBrick;
        var image = _parentField.GetComponent<Image>();
        var refPixelsPerUnit = ScreenAdjusment.RefPixelsPerUnit;
        return new Vector2Int( Mathf.RoundToInt(image.rectTransform.rect.width / (refPixelsPerUnit * scale)),
              Mathf.RoundToInt(image.rectTransform.rect.height / (refPixelsPerUnit * scale)));
    }

    public void SaveScaleValueBricks(TypesOption typeOption, WindowScalingBlocks windowScalingBlocks)
    {
        if (typeOption == TypesOption.SizeCells)
        {
            DataSetting.GameData.SetupOptionValue(TypesOption.SizeCells, windowScalingBlocks.ScaleBricks);
            _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        }
    }

    public void ReloadField()
    {
        GameState.StopGame();
        GameState.ResetTimeView();
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        foreach (Transform cell in _parentField)
        {
            if (cell.TryGetComponent(out CellView cellview))
            {
                foreach (Transform child in cellview.transform)
                {
                    cellview.BrickView.Despawn();
                    cellview.Despawn();
                }
            }
        }
        new FieldCells(this);
        _uiData.WindowWinner.Hide();
    }

    public void ActivateWindowsWin() => _uiData.WindowWinner.Display(_uiData.WindowTimer);


    public void DisplayCountMines(int countMines)
    {
        _uiData.WindowCountMines.Display(countMines);
    }

    public void Activate()
    {
    }

    public override void ConfirmAction(bool value)
    {
        if (value == true)
        {
            _gameState.BackPreviousWindow.Open(this);
            _gameState.StopGame();
        }

        _gameState.CurrentInitWindow(this);
    }

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand)
    {
        if (_gameState.IsPlay)
        {
            _gameState.UIData.WindowConfirmation.ActivateWindow(this);
        }
        else
            _gameState.BackPreviousWindow.Open(this);
    }

    public override void Hide()
    {
    }
}