
public class FlagDownAction : IDownAction
{
    private readonly FieldCells _fieldCells;
    private readonly ContainerMines _containerMines;
    private Cell _currentCell;

    public FlagDownAction(FieldCells fieldCells, ContainerMines containerMines )
    {
        _fieldCells = fieldCells;
        _containerMines = containerMines;
    }

    
    
      public bool Select( Cell cell )
      {
          _currentCell = cell;

          var isWin = _fieldCells.isWin();
          
          if (cell.IsOpen) return false;
          
          if (_containerMines == null || _containerMines.CountMines == 0) { return false;}

          if ( _containerMines.CountFlags > 0 || cell.IsFlagged == true)
              _fieldCells.GameField.UIData.WindowWindowCountMines.ActivateMoveFlag(cell.CellView);
              //_fieldCells.GameField.UIData.WindowWindowCountMines.ActivateMoveFlag(this);
          
          
          var result = cell.SetFlag(_containerMines);
          
          if( result ) _fieldCells.IncrementFlagCount();
          
          // эти строки раскоментить если анимация флага выключена
          //_fieldCells.GameField.DisplayCountMines(_containerMines.CountFlags);
          //_fieldCells.GameField.Sounds.PlayAudio(TypesAudio.SoundFlag);
          

          if ( _fieldCells.GameField.DataSetting.GameData.GetOptionValue(TypesOption.Vibration) == 1f )
          {
              _fieldCells.GameField.GameState.Vibrate( 500 );
          }

         // if (_fieldCells.isWin()) //здесь что то делать если анимация флага выключена (раскоментить в блоке)
           if( isWin )
        {
            //_fieldCells.ActivateWin();
        }
        return result;
    }

}