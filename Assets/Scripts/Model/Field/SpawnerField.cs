using UnityEngine;

public class SpawnerField
{
    private readonly FieldCells _fieldCells;
    private readonly ContainerMines _containerMines;
    private GameField _gameField;
    private readonly FieldCellData FieldCellData;
    private readonly Cell[,] _cells;
    private IDownAction _downAction;
    private FactoryViewPool<CellView> _factoryCellView;

    public SpawnerField(FieldCells fieldCells, Cell[,] cells)
    {
        _fieldCells = fieldCells;
        _cells = cells;
        FieldCellData = fieldCells.FieldCellData;
        _containerMines = fieldCells.ContainerMines;
        _factoryCellView = new FactoryViewPool<CellView>(fieldCells.GameField.PoolDataContainer.RootCells.PoolData.Pool,
            fieldCells.GameField.transform);
    }

    public void CreateBlocks()
    {
        _gameField = _fieldCells.GameField;
        for (var j = 0; j < FieldCellData.CountRows; j++)
        for (var i = 0; i < FieldCellData.CountColumns; i++)
        {
            var cellData = new CellData(i, j, FieldCellData.Scale);
            CellView cellView = _factoryCellView.Create();
            cellView.Init(_gameField, _fieldCells.GameField.GameState.Views, cellData);
            _cells[i, j] = new Cell(cellView);
            _cells[i, j].Create(cellView);
            _cells[i, j].CellView.InputHandler.OnActivateCell += ActionAfterActivateCell;
            _cells[i, j].CellView.InputHandler.OnActivateFlag += ActionAfterHoldCell;
        }
    }

    private void ActionAfterActivateCell(InputHandler inputHandler)
    {
        CellView cellView = inputHandler.CellView;

        _downAction = new DigDownAction(_fieldCells);


        if (_gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag)
        {
            _downAction = new FlagDownAction(_fieldCells, _containerMines);
        }

        if (_fieldCells.IsFirstClick)
        {
            if (_gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag)
            {
                return;
            }

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