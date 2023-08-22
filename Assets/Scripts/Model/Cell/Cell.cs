

public class Cell : ICell
{
    private CellView _cellView;
    public Flag Flag { get; private set; }
    public int Value { get; private set; }
    public bool IsOpen { get; private set; }
    public bool IsFlagged { get; private set; }
    public bool IsInitMine { get; private set; }
    public CellData CellData { get; private set; }
    public CellView CellView => _cellView;

    public Cell(CellView cellView)
    {
        Value = 0;
        _cellView = cellView;
        IsOpen = false;
        IsInitMine = false;
        IsFlagged = false;
        CellData = cellView.CellData;
        Flag = new Flag(_cellView);
    }

    public void Reset()
    {
        Value = 0;
        IsOpen = IsInitMine = IsFlagged = false;
        ResetValue();
    }

    public void CreateMine(int value)
    {
        Value = value;
        IsInitMine = (Value == -1);
    }

    public void Open()
    {
        IsOpen = true;
        if (CellView.BrickView != null)
            CellView.BrickView.SetActive(false);
    }

    public bool SetFlag(ContainerMines containerMines)
    {
        if (IsOpen == true) return true;
        Flag.SetFlag(containerMines);
        IsFlagged = Flag.Value;
        return IsFlagged && IsInitMine;
    }

    public void IncrementValue()
    {
        Value++;
        _cellView.SetTextNumbers(Value);
    }

    private void ResetValue()
    {
        _cellView.ResetSprite();
        _cellView.BrickView.SetActive(true);
        _cellView.MineView.SetActive(false);
        _cellView.InputHandler.enabled = true;
        _cellView.MineView.Reset();
        Flag.Reset();
    }

    public void Despawn()
    {
        _cellView.Despawn();
    }

    /*
    public void Spawn( Pool<CellView> pool, CellData cellData )
    {
        _cellView.SpawnFrom( pool );
        Value = 0;
        IsOpen = false;
        IsInitMine = false;
        IsFlagged = false;
        CellData = _cellView.CellData;
        Flag = new Flag(_cellView);
        ResetValue();
    }
    */

    public void Spawn(Pool<CellView> pool, CellData cellData)
    {
        _cellView = pool.Get();
        //_cellView.SpawnFrom( pool );
        Value = 0;
        IsOpen = false;
        IsInitMine = false;
        IsFlagged = false;
        CellData = _cellView.CellData;
        Flag = new Flag(_cellView);
        ResetValue();
    }

  
    
    
}