using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class FieldCells 
{
    private readonly GameField _gameField;
    private readonly Cell[,] _cells;
    private readonly int[] _firstIndexes;
    private SpawnerField _spawnerField;
    private int _countOpen;
    private int _countFlagTrue;
    public FieldCellData FieldCellData { get; private set; }
    public ContainerMines ContainerMines { get; private set; }
    private readonly int _percentMine = 15;
    private int _countCells; 
    public bool IsFirstClick { get; private set; }
    public ICell[,] Cells => _cells;
    public GameField GameField => _gameField;
    
    public FieldCells( GameField gameField )
    {
        _gameField = gameField;
        IsFirstClick = true;
        var widthPerUnit = gameField.GetSizePerUnit();
        var countColumns = Mathf.RoundToInt( widthPerUnit.x );
        if (countColumns > widthPerUnit.x) countColumns--;
        var countRows = Mathf.RoundToInt(widthPerUnit.y);
        if (Screen.width < Screen.height) countRows = countColumns;


        countColumns = gameField.GetCountBlocksXY().x;
        countRows = gameField.GetCountBlocksXY().y;

        
        
        FieldCellData = new FieldCellData(countColumns,countRows,_gameField.GameState.GameFieldData.ScaleBrick);
        _cells = new Cell[FieldCellData.CountColumns, FieldCellData.CountRows];
        _countCells = _cells.Length;
        _firstIndexes = new int[2] { -1, -1 };
        ContainerMines = new ContainerMines( this._gameField, _cells, _firstIndexes );
       _spawnerField = new SpawnerField(this, _cells );
        _spawnerField.CreateBlocks();
        
        
    }

    public void ConfirmFirstClick()
    {
        IsFirstClick = false;
    }

    public void Reset() => IsFirstClick = true;

    public void FindFirstIndexesOnClick( ICell cell )
    {
        _firstIndexes[0] = cell.CellData.Index1;
        _firstIndexes[1] = cell.CellData.Index2;
        IsFirstClick = false;
    }

    public void GenerateMines()
    {
        var countCells = FieldCellData.CountColumns * FieldCellData.CountRows;
        ContainerMines.GenerateMines( _percentMine, countCells );
    }
   
    public void InitGrid()
    {
        for( var i = 0 ; i < FieldCellData.CountColumns; i++ )
        for( var j = 0; j < FieldCellData.CountRows; j++)
        {
             if( _cells[i, j].Value != -1 )
             {
                for( int n = -1; n < 2 ; n++ )
                for( int m = -1; m < 2; m++ )
                {
                   if ( i + n >= 0 && j + m >= 0 &&
                        i + n <= _cells.GetLength(0)-1 &&
                        j + m <= _cells.GetLength(1)-1 &&
                        _cells[i + n, j + m].Value == -1 )
                   {
                       _cells[i,j].IncrementValue();
                   }
                }
             }
        }
    }

    public void IncrementFlagCount()
    {
        _countFlagTrue++;
    }

    public bool isWin() => (_countFlagTrue + _countOpen) >= _countCells;
    

    
    public bool TryOpen( ICell cell )
    {
        if (cell.IsOpen == true || cell.IsFlagged ) return true;

        cell.Open();
        _gameField.Sounds.PlayAudio(TypesAudio.SoundClick);
        _countOpen++;
        IMineView mineView = null;
        mineView = cell.CellView.MineView;

        if( cell.Value == 0 )
        {
            var index1 = cell.CellData.Index1;
            var index2 = cell.CellData.Index2;
            FindNeighbourEmptyCellsAndOpen(_cells, index1, index2);
            _gameField.Sounds.PlayAudio(TypesAudio.SoundEmpty);
        }
        else if (cell.Value == -1)
        {
            mineView.ActivateMine(true );
            _gameField.Sounds.PlayAudio(TypesAudio.SoundExplode);
            return false;
        }
        return true;
    }

    private int count = 0;
    private void FindNeighbourEmptyCellsAndOpen( ICell [,] cells, int index1, int index2 )
    {
        for( int n = -1; n < 2 ; n++ )
        for( int m = -1; m < 2; m++ )
        {
            if ( index1 + n >= 0 && index2 + m >= 0 &&
                 index1 + n <= FieldCellData.CountColumns-1 &&
                 index2 + m <= FieldCellData.CountRows-1 &&
              cells[index1 + n, index2 + m].Value >= 0 )
                TryOpen(cells[index1 + n, index2 + m]);
        }
    }
    
    public void OpenAll()
    {
        foreach (var cell in _cells)
        {
            if( cell.IsOpen ) continue; 
            cell.Open();
            if(cell.IsInitMine) cell.CellView.MineView.ActivateMine(false); 
            if (cell.IsInitMine == false && cell.CellView.FlagView.Value)
                cell.CellView.FlagView.SetFlagError();
            cell.CellView.InputHandler.enabled = false;
        }
    }
    
    
    public ICell[,] GetCells() => _cells;


}
