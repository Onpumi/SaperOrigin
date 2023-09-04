using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class ButtonMenu : WindowBase, IBackToPreviousWindowCommand, IPointerDownHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _activeWindowCommand;
    [SerializeField] private IBackToPreviousWindowCommand _backToPreviousWindowCommand;
    [SerializeField] private WindowSettings _windowSettings;
    [SerializeField] private IWindowCommand _backWindowCommand;
    [SerializeField] private MenuBarView _topMenu;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


    private void Start()
    {
        if (_topMenu != null)
        {
            _topMenu.FitSizeUI(_rectTransform);
        }

        transform.localScale = Vector2.one * 0.8f;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        var activesUI = new List<IWindowCommand>();
        activesUI.Add(_activeWindowCommand);
        Display(activesUI);
        if (_windowSettings != null)
        {
            if (_backToPreviousWindowCommand != null)
                _gameState.CurrentInitWindow(_windowSettings);
            _windowSettings.InitBackWindowCommand(_backWindowCommand);
        }
    }

    public override void Display(List<IWindowCommand> activeUI)
    {
        _gameState.ActivatePause(true);
        _gameState.DisableAllUI();
        if (activeUI != null)
            activeUI.ForEach(ui => ui.Enable());
    }

    public void Activate()
    {
    }
}