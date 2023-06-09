using UnityEngine;
using UnityEngine.EventSystems;
using YG;


public class BackButton : WindowBase, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private BackActionMenu _backActionMenu;
    [SerializeField] private IWindowCommand _windowCommand;
    [SerializeField] private IBackToPreviousWindowCommand _backToPreviousWindowCommand;

    public override void ConfirmAction( bool value )
    {
        if ( _backActionMenu == BackActionMenu.ReturnMainMenu && value == true )
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
                Application.ExternalCall("quit");
                //System.Reflection.Assembly.GetExecutingAssembly().GetType("UnityEngine.Application").GetMethod("Quit").Invoke(null, null);
                //YandexGame.SaveProgress();
            }
            else
            {
                Application.Quit();
            }
            //Application.OpenURL("about:blank");
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            
            //System.Reflection.Assembly.GetExecutingAssembly().GetType("UnityEngine.Application").GetMethod("Quit").Invoke(null, null);
            //var jsFunction = "ExitJS";
            //Application.ExternalCall(jsFunction);
      //      if (confirm("Вы действительно хотите закрыть окно браузера?")) {
//                window.close();
           // }

        }
        
    }
    

    public void OnPointerDown( PointerEventData eventData )
    {
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        TryBackAction();
        if( _windowCommand == _gameState.UIData.WindowPause ) return;
        if (_windowCommand != null)
        { 
            if( _windowCommand == _gameState.UIData.WindowSettings)
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
  

    }
    
    
}


public enum BackActionMenu
{
    ReturnPlayGame,
    ReturnMainMenu,
    Exit
}