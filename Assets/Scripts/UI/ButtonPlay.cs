using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPlay : WindowBase, IPointerDownHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private readonly List<IWindowCommand> _activeUI = new List<IWindowCommand>();
    [SerializeField] private Color _lossColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private IBackToPreviousWindowCommand _backToPreviousWindowCommand;
    [SerializeField] private IWindowCommand _backWindowCommand;
    private Image _image;


    public void OnEnable()
    {
        _image = GetComponent<Image>();
    }

    public void SetLossColor() => _image.color = _lossColor;
    public void SetNormColor() => _image.color = _normalColor;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameState.IsPlay)
            _gameState.UIData.WindowConfirmation.ActivateWindow(this);
        else
        {
            if (_gameState.Views.GameField.IsLoadPoolFinish)
                StartPlay();
        }

        _gameState.InitPreviousWindow(_backToPreviousWindowCommand);
        _gameState.CurrentInitWindow(_gameState.Views.GameField);
    }


    private void StartPlay()
    {
        _gameState.Views.GameField.ReloadField();
        Display(_activeUI);
    }

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand) => Open();

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }

    public override void ConfirmAction(bool value)
    {
        if (value == true)
        {
            StartPlay();
        }
    }

    public override void Display(List<IWindowCommand> activeUI)
    {
        _gameState.DisableAllUI();
        //      if( activeUI != null )
//          activeUI.ForEach(ui => ui.Enable());
    }
}