using UnityEngine;
using UnityEngine.EventSystems;


public class BackButton : WindowBase, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private BackActionMenu _backActionMenu;
    [SerializeField] private IWindowCommand _windowCommand;
    [SerializeField] private IBackToPreviousWindowCommand _backToPreviousWindowCommand;
    [SerializeField] private bool _isPlayGame;

    public override void ConfirmAction(bool value)
    {
        
        if (_backActionMenu == BackActionMenu.ReturnMainMenu && value == true)
        {
            _windowCommand.Hide();
            if (_backToPreviousWindowCommand != null)
            {
                _backToPreviousWindowCommand.Activate();
                return;
            }

            _gameState.BackPreviousWindow.Open(this, _gameState.UIData.MainWindow);
            _gameState.StopGame();
        }
        else if (_backActionMenu == BackActionMenu.Exit && value == true)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
            }
            else
            {
                Application.Quit();
            }
        }
        
        /*
        if (_isPlayGame == false)
        {
            _windowCommand.Hide();
            if (_backToPreviousWindowCommand != null)
            {
                _backToPreviousWindowCommand.Activate();
                return;
            }

            _gameState.BackPreviousWindow.Open(this, _gameState.UIData.MainWindow);
            _gameState.StopGame();
        }
        */
    }


    public void OnPointerDown(PointerEventData eventData)
    {
//        _backToPreviousWindowCommand.Activate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TryBackAction();
        ExitFromCurrentWindow();
    }

    private void ExitFromCurrentWindow()
    {
        if (_windowCommand == _gameState.UIData.WindowPause) return;
        if (_windowCommand != null)
        {
            if (_windowCommand == _gameState.UIData.WindowSettings)
                _gameState.BackPreviousWindow.Open(_windowCommand);
            _gameState.CurrentInitWindow(_windowCommand.BackWindowCommand);
        }
    }

    public void TryBackAction()
    {
        
        if (_backActionMenu != BackActionMenu.ReturnPlayGame)
        {
            _gameState.UIData.WindowConfirmation.ActivateWindow(this);
        }
        else
        {
            _windowCommand.Hide();
            _gameState.ActivatePause(false);
            _gameState.CurrentInitWindow(_windowCommand.BackWindowCommand);
        }
        
/*        
        if ( _isPlayGame == false )
        {
            _gameState.UIData.WindowConfirmation.ActivateWindow(this);
        }
        else
        {
            _windowCommand.Hide();
            _gameState.ActivatePause(false);
            _gameState.CurrentInitWindow(_windowCommand.BackWindowCommand);
        }
*/
    }
}


public enum BackActionMenu
{
    ReturnPlayGame,
    ReturnMainMenu,
    Exit
}