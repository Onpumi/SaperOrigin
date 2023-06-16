using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowScaleBricks : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private SliderSizeCells _sliderSizeCells;
    [SerializeField] private WindowScalingBlocks _windowScalingBlocks;
    [SerializeField] private IWindowCommand _backWindowCommand;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;
    private CanvasScaler _canvasScaler;

    private void OnEnable()
    {
        _gameState.CurrentInitWindow(this);
    }

    private void OnDisable()
    {
        _gameState.CurrentInitWindow( _backWindowCommand );
    }

    private void Start()
    {

        _canvasScaler = GetComponent<CanvasScaler>()
                             ?? throw new ArgumentException("Canvas Scaler is not available");
        Hide();
    }

    public override void OpenWindow()
    {
        Open();
        _sliderSizeCells.OpenMenuSizeCells();
    }

    public override void OpenCanvasByPressingEscape( IWindowCommand windowCommand )
    {
        _gameState.BackPreviousWindow.Open(this);
    }
    
    
}
   
