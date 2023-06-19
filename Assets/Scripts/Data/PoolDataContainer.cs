using UnityEngine;

public struct PoolDataContainer
{
    public readonly PoolDataRoot<CellView> RootCells;

    public PoolDataContainer( Views view, Transform parent, int size)
    {
        RootCells =
            new PoolDataRoot<CellView>( view.CellView, parent, size, nameof(view.CellView));
    }
    
}
