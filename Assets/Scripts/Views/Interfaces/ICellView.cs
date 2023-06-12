
using UnityEngine;
using System.Collections.Generic;

public interface ICellView 
{
    public MineView MineView { get; }
    public FlagView FlagView { get;  }
    public BrickView BrickView { get; }
    public void Init( GameField gameField, IViews views, CellData cellData);
    public bool InitFlag( bool value );
}
