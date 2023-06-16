using UnityEngine;
public interface ICell
{
    public int Value { get; }

    public bool IsOpen { get;  }
    public bool IsFlagged { get;  }
    public bool IsInitMine { get; }
    public void Open();
    public CellData CellData { get; }
    public CellView CellView { get;  }
    public bool SetFlag( ContainerMines containerMines );
    public void IncrementValue();
    public void CreateMine( int valueCell );

}
