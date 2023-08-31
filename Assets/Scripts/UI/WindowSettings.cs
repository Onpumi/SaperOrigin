
using System;
using UnityEngine;

public class WindowSettings : WindowBase , IWindowUI 
{
    [SerializeField] private GameState _gameState;
    private IBackToPreviousWindowCommand _backToPreviousWindowCommand;
    public IBackToPreviousWindowCommand BackToPreviousWindowCommand => _backToPreviousWindowCommand;
    private IWindowCommand _backWindowCommand;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        _gameState.CurrentInitWindow(this);

    }


    public void Start()
    {
        if (Screen.width > Screen.height)
        {
            var recTransform = GetComponent<RectTransform>();
            var offset = (recTransform.rect.width - recTransform.rect.height) * 0.5f;
         //   recTransform.anchorMin = new Vector2(0.3f, 0);
         //   recTransform.anchorMax = new Vector2(0.7f, 1f);
         recTransform.offsetMin = new Vector2(offset,0f);
         recTransform.offsetMax = new Vector2(-offset,0f);
        }
    }

    public void InitBackWindowCommand( IWindowCommand backWindowCommand )
    {
        _backWindowCommand = backWindowCommand;
    }

     public override void OpenCanvasByPressingEscape( IWindowCommand windowCommand )
    {
        _gameState.BackPreviousWindow.Open(this, _backWindowCommand );
        _gameState.ActivatePause(false);
    }
  
  
    
    public void InitBackNavigation(IBackToPreviousWindowCommand backToPreviousWindowCommand)
    {
        _backToPreviousWindowCommand = backToPreviousWindowCommand;
    }

    public void Activate(  )
    {
        Enable();
    }
    
    
}
