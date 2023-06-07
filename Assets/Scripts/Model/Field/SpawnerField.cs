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
        for (var i = 0; i < FieldCellData.CountColumns; i++)
        for (var j = 0; j < FieldCellData.CountRows; j++)
        {
            var cellData = new CellData(i, j, FieldCellData.Scale);
            var factoryCell = new FactoryCell(_gameField, cellData);
            _cells[i, j] = factoryCell.Create();
            _cells[i, j].CellView.InputHandler.OnClickCell += ActionAfterClickCell;
            _cells[i, j].CellView.InputHandler.OnClickDelay += ActionAfterHoldCell;
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