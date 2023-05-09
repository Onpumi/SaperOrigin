using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class UIData : MonoBehaviour
{
    [SerializeField] private List<IWindowCommand> _uis;  
    [SerializeField] private GameState _gameState;
    [SerializeField] private WindowTimer _windowTimer;
    [SerializeField] private ControllerButtonMode _buttonMode;
    [SerializeField] private WindowWinner _windowWinner;
    [SerializeField] private WindowCountMines _countMines;
    [SerializeField] private WindowConfirmation _windowConfirmation;
    [SerializeField] private ButtonPlay _buttonPlay;
    [SerializeField] private MainWindow _mainWindow;
    [SerializeField] private WindowSettings _windowSettings;
    [SerializeField] private WindowPause _windowPause;

    public WindowTimer WindowTimer => _windowTimer;
    public ControllerButtonMode ControllerButtonMode => _buttonMode;
    public WindowWinner WindowWinner => _windowWinner;
    public WindowCountMines WindowCountMines => _countMines;
    public WindowConfirmation WindowConfirmation => _windowConfirmation;
    public ButtonPlay ButtonPlay => _buttonPlay;
    public GameState GameState => _gameState;
    public MainWindow MainWindow => _mainWindow;
    public WindowSettings WindowSettings => _windowSettings;
    public WindowPause WindowPause => _windowPause;
}