using UnityEngine;

public class WindowPause : WindowBase 
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _backWindowCommand;
    [SerializeField] private WindowConfirmation _windowConfirmation;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand)
    {
        _gameState.BackPreviousWindow.Open(this);   
    }

    public override void Hide()
    {
        base.Hide();
        _windowConfirmation.Hide();
        _gameState.ActivatePause(false);
    }
  

}
