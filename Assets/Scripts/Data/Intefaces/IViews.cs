using System.Collections.Generic;

public interface IViews
{
    public MineView MineView { get; }
    public CellView CellView { get;  }
    public BrickView BrickView { get; }
    public FlagView FlagView { get; }
    public GameField GameField { get; }
    public List<IView> View { get; }
}
