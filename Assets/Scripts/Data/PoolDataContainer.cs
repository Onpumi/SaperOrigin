using UnityEngine;

public struct PoolDataContainer
{
    public readonly PoolDataRoot<CellView> RootCells;
    //public readonly PoolDataRoot<BrickView> RootBricks;
    public readonly PoolDataRoot<MineView> RootMines;
    public readonly PoolDataRoot<FlagView> RootFlags;
    //public readonly PoolDataRoot<BorderField> BorderField;

    public PoolDataContainer( Views view, Transform parent, int size)
    {
        RootCells =
            new PoolDataRoot<CellView>( view.CellView, parent, size, nameof(view.CellView));
      //  RootBricks =
        //    new PoolDataRoot<BrickView>(view.BrickView, parent, size, nameof(view.BrickView));
        RootMines =
            new PoolDataRoot<MineView>(view.MineView, parent, size, nameof(view.MineView));
        RootFlags =
            new PoolDataRoot<FlagView>(view.FlagView, parent, size, nameof(view.FlagView));
        //RootBorder = new PoolDataRoot<FlagView>(view.FlagView, parent, size, nameof(view.FlagView));
        
    }
    
}
