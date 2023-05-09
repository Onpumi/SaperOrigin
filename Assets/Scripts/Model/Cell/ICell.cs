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
    //public void Display( Vector3 positionStart, float scale);
    public void Display( int indexI, int indexJ, float scale);

}
