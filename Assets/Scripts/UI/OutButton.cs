using UnityEngine;
using UnityEngine.EventSystems;

public class OutButton : WindowBase, IPointerDownHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _windowNextCommand;
    [SerializeField] private IWindowCommand _windowPrevCommand;


    private void ActivateCommandWindow()
    {
        _windowPrevCommand.Hide();
        _windowNextCommand.Enable();
    }
    public override void ConfirmAction(bool value)
    {
        if (value == true)
        {
            ActivateCommandWindow();
            if (_windowNextCommand is MainWindow)
            {
                _gameState.StopGame();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateAction();
    }

    private void ActivateAction()
    {
        if (_windowPrevCommand is WindowPause)
        {
            if (_gameState.IsOpenField && _gameState.IsPlay)
                _gameState.UIData.WindowConfirmation.ActivateWindow(this);
            else 
                ActivateCommandWindow();
            return;
        }
        else if (_windowPrevCommand is WindowSettings)
        {
            if (_gameState.IsOpenField == true)
            {
                _gameState.UIData.WindowSettings.Hide();
                _gameState.ActivatePause(false);
            }
            else
            {
                _gameState.UIData.MainWindow.Enable();
                _gameState.UIData.WindowSettings.Hide();
            }

            return;
        }

        if (_windowNextCommand is not GameField)
        {
            _windowNextCommand.Enable();
        }
        else
        {
            _gameState.ActivatePause(false);
        }

        _windowPrevCommand.Hide();
    }
}