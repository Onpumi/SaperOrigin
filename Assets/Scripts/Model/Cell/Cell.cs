
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

    public void Create( CellView cellView)
    {
        //_cellView = cellView;
    }
    
    
}