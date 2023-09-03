using UnityEngine;

public class MainWindow : WindowBase, IBackToPreviousWindowCommand
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private ClassForJavaScript _js;
    private bool _isReadyExit = true; 
    
    public override void OpenCanvasByPressingEscape( IWindowCommand windowCommand )
    {
       if (transform.gameObject.activeSelf)
       {
           if (_isReadyExit)
           {
               _gameState.QuitGame();
           }
           _isReadyExit = !_isReadyExit;
       }
    }

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //_js.Test();
        //Resolution resolution = Screen.currentResolution;
        //int width = resolution.width;
        //int height = resolution.height;
        //string jsFunction = "DisplayResolution";
        //Application.ExternalCall(jsFunction, width, height);
#endif
        
    }
    private void OnDisable()
    {
        _isReadyExit = false;
    }

    public void Activate( )
    {
            Enable();
            ActivateChildAll(transform);
            _gameState.StopGame();
    }

    private void ActivateChildAll(Transform transformParent)
    {
        foreach (Transform child in transformParent )
        {
            child.gameObject.SetActive(true);
            if(child.childCount > 0) ActivateChildAll(child);
        }
    }

    public override void Enable()
    {
        base.Enable();
        ActivateChildAll(transform);
        _gameState.CloseField();
        
    }

  

    
}
