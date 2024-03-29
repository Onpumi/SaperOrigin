using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowConfirmation : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Button _buttonConfirm;
    [SerializeField] private Button _buttonCancel;
    private IWindowCommand _activeWindowCommand;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        _buttonConfirm.onClick.AddListener(delegate
        {
            Hide();
            _activeWindowCommand.ConfirmAction(true);
        });
        _buttonCancel.onClick.AddListener(delegate
        {
            Hide();
            _activeWindowCommand.ConfirmAction(false);
        });
        if ( _gameState != null && _gameState.IsPause == false)
            _gameState.ActivatePause(true);
    }

    public void ActivateWindow(IWindowCommand windowCommand)
    {
        Open();
        _activeWindowCommand = windowCommand;
    }


    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand) => Hide();
}