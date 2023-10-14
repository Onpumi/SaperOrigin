using UnityEngine;

public class FieldCells
{
    private readonly GameField _gameField;
    private readonly Cell[,] _cells;
    private readonly int[] _firstIndexes;
    private readonly SpawnerField _spawnerField;
    private const int MaxCountCellInRowOrColumn = 1000;
    private int _countOpen;
    private int _countFlagTrue;
    public FieldCellData FieldCellData { get; private set; }
    public ContainerMines ContainerMines { get; private set; }
    private readonly int _percentMine = 15;
    private int _countCells;
    public bool IsFirstClick { get; private set; }
    public Cell[,] Cells => _cells;
    public GameField GameField => _gameField;

    public FieldCells(GameField gameField, int countColumns, int countRows)
    {
        _gameField = gameField;
        IsFirstClick = true;

        if (Screen.width > Screen.height)
        {
            countColumns = (int)(countColumns / (int)(countColumns / countRows) / 1.5f);
            (countColumns, countRows) = _gameField.BackGroundField.UpdatePropertiesInGrid(countColumns);
        }

        FieldCellData = new FieldCellData(countColumns, countRows, new Vector2(1, 1));
        _cells = new Cell[ MaxCountCellInRowOrColumn, MaxCountCellInRowOrColumn];
        _countCells = countColumns * countRows;
        _firstIndexes = new int[2] { -1, -1 };
        ContainerMines = new ContainerMines(this._gameField, _cells, _firstIndexes);
        _spawnerField = new SpawnerField(this, _cells);
        _spawnerField.CreateBlocks();
    }


    public void ResetField(int countColumns, int countRows, float scale)
    {
        if (Screen.width > Screen.height)
        {
            countColumns = (int)(countColumns / (int)(countColumns / countRows) / 1.5f);
            (countColumns, countRows) = _gameField.BackGroundField.UpdatePropertiesInGrid(countColumns);
        }

        IsFirstClick = true;
        _firstIndexes[0] = -1;
        _firstIndexes[1] = -1;
        FieldCellData = new FieldCellData(countColumns, countRows, new Vector2(1, 1));
        _countCells = countColumns * countRows;
        IsFirstClick = true;
        _countFlagTrue = 0;
        _countOpen = 0;
        ContainerMines = new ContainerMines(this._gameField, _cells, _firstIndexes);
        _spawnerField.Init(this);
        _spawnerField.ResetBlocs(this);
    }

    public void DespawnField()
    {
        var countRows = _cells.GetLength(0);
        var countColumns = _cells.GetLength(1);
        for (int i = 0; i < countRows; i++)
        for (int j = 0; j < countColumns; j++)
            if (_cells[i, j] != null)
            {
                _cells[i, j].Despawn( _gameField.Pool );
            }
    }
    
   
    public void ConfirmFirstClick()
    {
        IsFirstClick = false;
    }

    public void Reset() => IsFirstClick = true;

    public void FindFirstIndexesOnClick(ICell cell)
    {
        _firstIndexes[0] = cell.CellData.Index1;
        _firstIndexes[1] = cell.CellData.Index2;
        IsFirstClick = false;
    }

    public void GenerateMines()
    {
        var countCells = FieldCellData.CountColumns * FieldCellData.CountRows;
        ContainerMines.GenerateMines(_percentMine, countCells, FieldCellData);
    }

    public void InitGrid()
    {
        for (var i = 0; i < FieldCellData.CountRows; i++)
        for (var j = 0; j < FieldCellData.CountColumns; j++)
        {
            if (_cells[i, j].Value != -1)
            {
                for (int n = -1; n < 2; n++)
                for (int m = -1; m < 2; m++)
                {
                    if (i + n >= 0 && j + m >= 0 &&
                        i + n <= FieldCellData.CountRows - 1 &&
                        j + m <= FieldCellData.CountColumns - 1 &&
                        _cells[i + n, j + m].Value == -1)
                    {
                        _cells[i, j].IncrementValue();
                    }
                }
            }
        }
    }

    public void IncrementFlagCount()
    {
        _countFlagTrue++;
    }

    public bool isWin()
    {
        return (_countFlagTrue + _countOpen) >= _countCells;
    }

    public bool TryOpen(ICell cell)
    {
        if (cell == null) return false;
        if (cell.IsOpen == true || cell.IsFlagged) return true;

        cell.Open();
        _gameField.Sounds.PlayAudio(TypesAudio.SoundClick);
        _countOpen++;
        IMineView mineView = null;
        mineView = cell.CellView.MineView;

        if (cell.Value == 0)
        {
            var index1 = cell.CellData.Index1;
            var index2 = cell.CellData.Index2;
            FindNeighbourEmptyCellsAndOpen(_cells, index1, index2);
            _gameField.Sounds.PlayAudio(TypesAudio.SoundEmpty);
        }
        else if (cell.Value == -1)
        {
            mineView.ActivateMine(true);
            _gameField.Sounds.PlayAudio(TypesAudio.SoundExplode);
            return false;
        }

        return true;
    }

    private void FindNeighbourEmptyCellsAndOpen(ICell[,] cells, int index1, int index2)
    {
        for (int n = -1; n < 2; n++)
        for (int m = -1; m < 2; m++)
        {
            if (index1 + n >= 0 && index2 + m >= 0 &&
                index1 + n <= FieldCellData.CountRows - 1 &&
                index2 + m <= FieldCellData.CountColumns - 1 &&
                cells[index1 + n, index2 + m].Value >= 0)
                TryOpen(cells[index1 + n, index2 + m]);
        }
    }

    public void ActivateWin()
    {
        GameField.GameState.StopGame(); 
        GameField.GameState.UIData.ButtonPlay.SetNormColor();
        GameField.SaveStatistics( true );
        OpenAll();
        Reset();
        GameField.ActivateWindowsWin();
    }
    

    public void OpenAll()
    {
        foreach (var cell in _cells)
        {
            if (cell is null || cell.IsOpen) continue;
            cell.Open();
            if (cell.IsInitMine) cell.CellView.MineView.ActivateMine(false);
            if (cell.IsInitMine == false && cell.CellView.FlagView.Value)
                cell.CellView.FlagView.SetFlagError();
            cell.CellView.InputHandler.enabled = false;
        }
    }


    public ICell[,] GetCells() => _cells;
}