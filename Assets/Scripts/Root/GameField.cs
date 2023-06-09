using System;
using System.Collections;
using UnityEngine;

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
    public PoolDataContainer PoolDataContainer { get; private set; }

    private FieldCells _fieldCells;
    public Pool<CellView> Pool { get; private set;  }

    public bool IsLoadPoolFinish { get; private set; }
    private IPoolFactory<CellView> _factoryCellViewPool;
    public IPoolFactory<CellView> FactoryCellViewPool => _factoryCellViewPool;



    private void Awake()
    {
        IsLoadPoolFinish = false;
        //PoolDataContainer = new PoolDataContainer(_views, _parentField, 500);
        //_factoryCellView = new FactoryViewPool<CellView>(PoolDataContainer.RootCells.PoolData.Pool, transform);
        
        //_factoryCellViewPool = new PrefabFactory<CellView>(Views.CellView, transform, name);
        //Pool = new Pool<CellView>(_factoryCellViewPool, 0);
        
        _factoryCellViewPool = new PrefabFactory<CellView>(Views.CellView, transform);
        Pool = new Pool<CellView>(_factoryCellViewPool, 3);

   //     var a = Pool.Get();
   //     var b = Pool.Get();
   //     a.Despawn();
   //     a.SpawnFrom(Pool);

    }

    private void Init()
    {
        BackGroundField = _parentField.GetComponent<BackgroundField>();
        DataSetting = new DataSetting(this);
        _gameState.GameFieldData.ScaleBrick = DataSetting.GameData.GetOptionValue(TypesOption.SizeCells);
        SetPercentMine((TypesGame)DataSetting.GameData.GetDifficultValue());
        var parent = _parentField;
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

        if (_fieldCells == null)
        {
            _fieldCells = new FieldCells(this, countColumns, countRows);
        }
        else
        {
            _fieldCells.CreateField(this, countColumns, countRows);
        }
        //else
        //{
        //    _fieldCells.ResetField();
        // }

        //BackGroundField.BorderField.Init(BackGroundField.RectTransform);
        //if (Screen.width > Screen.height)
        //  BackGroundField.FitSizeMenu();
        _uiData.WindowWinner.Hide();
    }


    public void StartProgressLoad()
    {
        StartCoroutine(StartProgress());
    }


    IEnumerator StartProgress()
    {
        _sliderProgress.transform.gameObject.SetActive(true);
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