

public class FactoryCell : IFactoryCell
{
    private FactoryViewPool<CellView> _factoryCellView;
    private CellData _cellData;
    private readonly Views _views;
    private GameField _gameField;

    public FactoryCell( GameField gameField,  CellData cellData)
    {
        _cellData = cellData;
        _factoryCellView = new FactoryViewPool<CellView>(gameField.PoolDataContainer.RootCells.PoolData.Pool, gameField.transform);
     //   _factoryCellView = new FactoryViewPool<CellView>(gameField.PoolDataContainer.RootCells.PoolData.Pool, gameField.ParentFieldTest);
        _views = gameField.Views;
        _gameField = gameField;
    }
    
    public Cell Create()
    {
        CellView cellView = _factoryCellView.Create();
        cellView.Init(  _gameField, _views, _cellData);
        return new Cell( cellView );
    }
}
