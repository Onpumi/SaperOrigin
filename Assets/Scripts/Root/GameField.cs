using System;
using UnityEngine;

public class GameField : WindowBase, IGameField, IBackToPreviousWindowCommand
{
    [SerializeField] private UIData _uiData;
    [SerializeField] private Views _views;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sounds _sounds;
    [SerializeField] private IWindowCommand _backWindowCommand;
    [SerializeField] private Transform _parentField;
    public BackgroundField BackGroundField { get; private set; }
    public DataSetting DataSetting { get; private set; }
    public UIData UIData => _uiData;
    public Views Views => _views;
    public GameState GameState => _gameState;
    public Sounds Sounds => _sounds;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;
    public PoolDataContainer PoolDataContainer { get; private set; }

    private void Init()
    {
        BackGroundField = _parentField.GetComponent<BackgroundField>();
        DataSetting = new DataSetting(this);
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        SetPercentMine((TypesGame)DataSetting.GameData.GetDifficultValue());
        var parent = _parentField;
        PoolDataContainer = new PoolDataContainer(_views, parent, 5000);
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
    }

    public float GetScale() => 1f;

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
        if (GameState == null) return;
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
        
        BackGroundField.Init(this);
        var scale = GameState.GameFieldData.ScaleBrick;
        var (countColumns, countRows) = BackGroundField.InitGRID(100f * scale);
        new FieldCells(this, countColumns, countRows);
        BackGroundField.BorderField.Init(BackGroundField.RectTransform);
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