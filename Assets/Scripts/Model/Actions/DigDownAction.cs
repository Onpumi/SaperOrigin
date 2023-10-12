
using UnityEngine;
using UnityEngine.UI;

public class DigDownAction : IDownAction
{
    private readonly FieldCells _fieldCells;

    public DigDownAction( FieldCells fieldCells )
    {
        _fieldCells = fieldCells;
    }

    public bool Select( Cell cell )
    {
        var result = _fieldCells.TryOpen( cell );
        if (result == false || (_fieldCells.isWin() && cell.IsInitMine == false)) StopGame( cell );
        return result;
    }

    public void StopGame( Cell cell )
    {
        _fieldCells.GameField.GameState.StopGame();
        _fieldCells.OpenAll();
        var isWin = _fieldCells.isWin();
        if ( isWin && cell.IsInitMine == false)
        {
            _fieldCells.GameField.ActivateWindowsWin();
            _fieldCells.GameField.GameState.UIData.ButtonPlay.SetNormColor();    
        }
        else
        {
            _fieldCells.GameField.GameState.UIData.ButtonPlay.SetLossColor();
        }
        _fieldCells.GameField.SaveStatistics( isWin );
    }
}
