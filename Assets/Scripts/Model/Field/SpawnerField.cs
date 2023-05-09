using System;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class SpawnerField
{
    private readonly FieldCells _fieldCells;
    private readonly ContainerMines _containerMines;
    private GameField _gameField;
    private readonly FieldCellData FieldCellData;
    private readonly Cell[,] _cells;
    private IDownAction _downAction;

    public SpawnerField(FieldCells fieldCells, Cell[,] cells)
    {
        _fieldCells = fieldCells;
        _cells = cells;
        FieldCellData = fieldCells.FieldCellData;
        _containerMines = fieldCells.ContainerMines;
    }

    public void CreateBlocks()
    {
        _gameField = _fieldCells.GameField;
        var camera = _gameField.CameraField;
        if (camera is null) throw new ArgumentException("Current camera is null");
        var deltaY = _gameField.ScreenAdjusment.PixelsPerUnit / _gameField.SpriteData.Height;
        var deltaX = _gameField.ScreenAdjusment.PixelsPerUnit / _gameField.SpriteData.Width;
        var resolutionCanvas = _gameField.ScreenAdjusment.ResolutionCanvas;
        var heightSprite = _gameField.SpriteData.Height * FieldCellData.Scale * deltaY;
        var widthSprite = _gameField.SpriteData.Width * FieldCellData.Scale * deltaX;
        var tabLeftForSprite = (resolutionCanvas.x - (float)FieldCellData.CountColumns * widthSprite) / 2f;
        var tabTopForSprite = resolutionCanvas.y * 0.01f;
        var positionStart = camera.ScreenToWorldPoint(new Vector3(tabLeftForSprite + widthSprite / 2f,
            tabTopForSprite + heightSprite / 2f + Screen.height * 0.25f));

        if (Screen.width > Screen.height)
            positionStart = camera.ScreenToWorldPoint(new Vector3(tabLeftForSprite + widthSprite / 2f,
                tabTopForSprite + heightSprite / 2f));
        Stopwatch stopwatch = new Stopwatch();
        
        for (var i = 0; i < FieldCellData.CountColumns; i++)
        for (var j = 0; j < FieldCellData.CountRows; j++)
        {
            var cellData = new CellData(i, j, FieldCellData.Scale);
            var factoryCell = new FactoryCell(_gameField, cellData);
            _cells[i, j] = factoryCell.Create();
            _cells[i, j].CellView.InputHandler.OnClickCell += ActionAfterClickCell;
            _cells[i, j].CellView.InputHandler.OnClickDelay += ActionAfterHoldCell;
            //_cells[i, j].Display(positionStart, FieldCellData.Scale);
            _cells[i, j].Display(i, j, FieldCellData.Scale);
        }
        
    }

    private void ActionAfterClickCell(InputHandler inputHandler)
    {
        CellView cellView = inputHandler.CellView;

        _downAction = new DigDownAction(_fieldCells);

        if (_gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag)
        {
            _downAction = new FlagDownAction(_fieldCells, _containerMines);
        }

        if (_fieldCells.IsFirstClick)
        {
            _gameField.GameState.StartGame();
            cellView.InitAction(_fieldCells, new FirstDigDownAction(_fieldCells));
        }

        cellView.InitAction(_fieldCells, _downAction);
    }

    private void ActionAfterHoldCell(InputHandler inputHandler)
    {
        if (inputHandler.transform.TryGetComponent(out CellView cellView) == false) return;
        _downAction = new FlagDownAction(_fieldCells, _containerMines);
        if (_gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag)
        {
            _downAction = new DigDownAction(_fieldCells);
        }

        cellView.InitAction(_fieldCells, _downAction);
    }
}