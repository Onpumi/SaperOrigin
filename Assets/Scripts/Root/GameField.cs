using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameField : WindowBase, IGameField, IBackToPreviousWindowCommand
{
    [SerializeField] private UIData _uiData;
    [SerializeField] private Views _views;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sounds _sounds;
    [SerializeField] private IWindowCommand _backWindowCommand;
    [SerializeField] private Transform _parentField;
    [SerializeField] private SliderProgress _sliderProgress;
    public BackgroundField BackGroundField { get; private set; }
    public DataSetting DataSetting { get; private set; }
    public UIData UIData => _uiData;
    public Views Views => _views;
    public GameState GameState => _gameState;
    public Sounds Sounds => _sounds;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;

    private FieldCells _fieldCells;
    public TypesGame DifficultLevel { get; private set; }
    public FieldCells FieldCells => _fieldCells;
    public Pool<CellView> Pool { get; private set; }
    public bool IsLoadPoolFinish { get; private set; }
    private IPoolFactory<CellView> _factoryCellViewPool;

    private bool _isFirstLoad = true;


    private void Awake()
    {
        IsLoadPoolFinish = false;
        _factoryCellViewPool = new PrefabFactory<CellView>(Views.CellView, transform);
        GeneratePool();
    }


    public void GeneratePool()
    {
        Pool = new Pool<CellView>(_factoryCellViewPool, 500);
    }

    private void Init()
    {
        BackGroundField = _parentField.GetComponent<BackgroundField>();
        DataSetting = new DataSetting(this);
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        SetPercentMine((TypesGame)DataSetting.GameData.GetDifficultValue());
    }

    public void SetPercentMine(TypesGame typesGame)
    {
        DifficultLevel = typesGame;
        DataSetting.UpdateStatisticsData(this );
        switch (typesGame)
        {
            case TypesGame.HardGame:
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
        StartCoroutine(StartProgress());
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

    public void ResetField()
    {
        _isFirstLoad = true;
        _fieldCells = null;
    }

    public void ReloadField()
    {
        if (GameState == null) return;
        GameState.StopGame();
        GameState.ResetTimeView();
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        BackGroundField.Init(this);
        var scale = GameState.GameFieldData.ScaleBrick;
        if (_fieldCells != null)
            _fieldCells.DespawnField();

        var (countColumns, countRows) = BackGroundField.InitGRID(100f * scale);

        if (_fieldCells == null || _isFirstLoad)
        {
            _fieldCells = new FieldCells(this, countColumns, countRows);
            _isFirstLoad = false;
        }
        else
        {
            _fieldCells.ResetField(countColumns, countRows, scale);
        }

        GameState.ResetCountMinesView(_fieldCells.ContainerMines.CountMines);

        BackGroundField.FitSizeMenu();
        _uiData.WindowWinner.Hide();
    }

    public void DestroyAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.transform.gameObject);
        }
    }

    IEnumerator StartProgress()
    {
        _sliderProgress.transform.gameObject.SetActive(true);
        _sliderProgress.SetSizes();
        for (int i = 0; i < 100; i++)
        {
            float progress = Mathf.Clamp01(i / 100f);
            _sliderProgress.UpdateValue(progress);
            yield return null;
        }

        _sliderProgress.UpdateValue(1);
        IsLoadPoolFinish = true;
        _sliderProgress.transform.gameObject.SetActive(false);
    }


    public void ActivateWindowsWin() => _uiData.WindowWinner.Display(_uiData.WindowTimer);


    public void SaveStatistics( bool isWin )
    {
        var totalSeconds = _uiData.WindowTimer.GetTotalSeconds();
        DataSetting.StatisticsData.UpdateCountFinishPlayGames(isWin);
        
        if ( isWin )
        {
            DataSetting.StatisticsData.UpdateTotalPlayGamesSeconds((int)totalSeconds);
            DataSetting.StatisticsData.CalculateAverageTime();
            DataSetting.StatisticsData.SetupBestPlayGames( (int)totalSeconds );
        }

    }

    public void DisplayCountMines(int countMines)
    {
        _uiData.WindowWindowCountMines.Display(countMines);
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
            _gameState.UIData.WindowConfirmation.ActivateWindow(this);
        else
            _gameState.BackPreviousWindow.Open(this);
    }

    public override void Hide()
    {
    }
}