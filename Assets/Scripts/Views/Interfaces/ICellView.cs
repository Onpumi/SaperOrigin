
using UnityEngine;
using System.Collections.Generic;

public interface ICellView : IView
{
    public MineView MineView { get; }
    public FlagView FlagView { get;  }
    public BrickView BrickView { get; }
    public void Display(ICell cell, Vector3 positionStart, float scale );
    public void Init( GameField gameField, IViews views, CellData cellData);
    public bool InitFlag( bool value );
}
