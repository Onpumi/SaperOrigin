using UnityEngine;

public class WindowSettings : WindowBase, IWindowUI
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Color _colorHighLightMenu;
    private CustomizerWindow _customizerWindow;
    private IBackToPreviousWindowCommand _backToPreviousWindowCommand;
    public IBackToPreviousWindowCommand BackToPreviousWindowCommand => _backToPreviousWindowCommand;
    private IWindowCommand _backWindowCommand;
    public override IWindowCommand BackWindowCommand => _backWindowCommand;
    public Color ColorHighLightMenu => _colorHighLightMenu;

    private void Awake()
    {
        Hide();
        _customizerWindow = new CustomizerWindow();
    }

    private void OnEnable()
    {
        _gameState.CurrentInitWindow(this);
    }


    public void Start()
    {
        _customizerWindow.InitSizeWindow(transform);
    }

    public void InitBackWindowCommand(IWindowCommand backWindowCommand)
    {
        _backWindowCommand = backWindowCommand;
    }

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand)
    {
        _gameState.BackPreviousWindow.Open(this, _backWindowCommand);
        _gameState.ActivatePause(false);
    }


    public void InitBackNavigation(IBackToPreviousWindowCommand backToPreviousWindowCommand)
    {
        _backToPreviousWindowCommand = backToPreviousWindowCommand;
    }

    public void Activate()
    {
        Enable();
    }
}