

using System;
using UnityEngine;

public class FlagDownAction : IDownAction
{
    private readonly FieldCells _fieldCells;
    private readonly ContainerMines _containerMines;

    public FlagDownAction(FieldCells fieldCells, ContainerMines containerMines )
    {
        _fieldCells = fieldCells;
        _containerMines = containerMines;
    }

      public bool Select( ICell cell )
      {
          if (cell.IsOpen) return false;
          
          if (_containerMines == null || _containerMines.CountMines == 0) { return false;}
          var result = cell.SetFlag(_containerMines);
          
          
          if( result ) _fieldCells.IncrementFlagCount();
          _fieldCells.GameField.DisplayCountMines(_containerMines.CountFlags);
          _fieldCells.GameField.Sounds.PlayAudio(TypesAudio.SoundFlag);

          if ( _fieldCells.GameField.DataSetting.GameData.GetOptionValue(TypesOption.Vibration) == 1f )
          {
              _fieldCells.GameField.GameState.Vibrate( 500 );
          }

          if (_fieldCells.isWin())
        {
            _fieldCells.GameField.GameState.StopGame(); 
            _fieldCells.GameField.GameState.UIData.ButtonPlay.SetNormColor();
            _fieldCells.OpenAll();
            _fieldCells.Reset();
            _fieldCells.GameField.ActivateWindowsWin();
        }
        return result;
    }

}