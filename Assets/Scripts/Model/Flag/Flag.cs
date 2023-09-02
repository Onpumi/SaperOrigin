public class Flag
{
    private ICellView _cellView;
    public bool Value { get; private set; }

    public Flag(ICellView cellView)
    {
        Value = false;
        _cellView = cellView;
    }

    public bool SetFlag(ContainerMines containerMines)
    {
        var countFlags = containerMines.CountFlags;


        if (countFlags <= 0 && Value == false)
        {
            return Value;
        }
        else if (countFlags <= 0 && Value == true)
        {
            Value = false;
            RemoveFlag();
            containerMines.SetCountFlags(1);
            return true;
        }

        else if (countFlags > 0 && Value == false)
        {
            containerMines.SetCountFlags(-1);
            return AddFlag();
        }

        else if (countFlags > 0 && Value == true)
        {
            containerMines.SetCountFlags(1);
            return RemoveFlag();
        }

        else return Value;
    }

    public void Reset()
    {
        RemoveFlag();
        _cellView.FlagView.ResetSprite();
    }

    private bool RemoveFlag() => Value = _cellView.InitFlag(false);

    private bool AddFlag() => Value = _cellView.InitFlag(true);
}