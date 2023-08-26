using UnityEngine;
using TMPro;

public class WindowWinner : WindowBase
{

    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _buttonPlay;
    [SerializeField] private TMP_Text _textTimeResult;
    [SerializeField] private WindowTimer _windowTimer;
    private const string TitleTime = "Время игры составило: ";
    private void Awake()
    {
         Hide();
    }

    public void Display( WindowTimer windowTimer )
    {
        Open();
        transform.SetAsLastSibling();
        _buttonPlay.Enable();
        _textTimeResult.text = TitleTime + _windowTimer.GetTimeResult();
    }

    public override void OpenCanvasByPressingEscape( IWindowCommand windowCommand ) => Hide();
}
