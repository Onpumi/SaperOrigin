using UnityEngine;
using TMPro;

public class WindowWinner : WindowBase
{

    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _buttonPlay;
    [SerializeField] private TMP_Text _textTimeResult; 
    
    private void Awake()
    {
         Hide();
    }

    public void Display( WindowTimer windowTimer )
    {
        Open();
        transform.SetAsLastSibling();
        _buttonPlay.Enable();
        //_textTimeResult.text = _textTimeResult.text + windowTimer.TimeValue;
    }

    public override void OpenCanvasByPressingEscape( IWindowCommand windowCommand ) => Hide();
}
