using UnityEngine;

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
        for (var j = 0; j < FieldCellData.CountRows; j++)
        for (var i = 0; i < FieldCellData.CountColumns; i++)
        {
            var cellData = new CellData(i, j, FieldCellData.Scale);
            var factoryCell = new FactoryCell(_gameField, cellData);
            
            _cells[i, j] = factoryCell.Create();
            _cells[i, j].CellView.InputHandler.OnActivateCell += ActionAfterActivateCell;
            _cells[i, j].CellView.InputHandler.OnActivateFlag += ActionAfterHoldCell;
            _cells[i, j].Display(i, j, FieldCellData.Scale);
        }
    }

    private void ActionAfterActivateCell(InputHandler inputHandler)
    {
        CellView cellView = inputHandler.CellView;

        _downAction = new DigDownAction(_fieldCells);
        
        //Debug.Log(_gameField.UIData.ControllerButtonMode.Mode);

        if (_gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag)
        {
            _downAction = new FlagDownAction(_fieldCells, _containerMines);
        }

        if ( _fieldCells.IsFirstClick )
        {
            if( _gameField.UIData.ControllerButtonMode.Mode == ButtonMode.Flag )
            {
                //Debug.Log("попытка установить флаг");
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