using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPause : WindowBase, IPointerDownHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private WindowPause _windowPause;
    private bool IsPause => !_windowPause.gameObject.activeSelf;

    private void Awake()
    {
        _windowPause.gameObject.SetActive( IsPause );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _gameState.ActivatePause(IsPause);
        _windowPause.gameObject.SetActive(IsPause);
        _windowPause.EnableFull();
        _gameState.CurrentInitWindow(_windowPause);
    }
   
  
}
