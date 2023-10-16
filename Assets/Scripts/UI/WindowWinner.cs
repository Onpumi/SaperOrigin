using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class WindowWinner : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _buttonPlay;
    [SerializeField] private TMP_Text _textTimeResult;
    [SerializeField] private TMP_Text _textRecordTimeResult;
    [SerializeField] private TMP_Text _textAverageTimeResult;
    [SerializeField] private TMP_Text _textCountLoss;
    [SerializeField] private TMP_Text _textCountWins;
    [SerializeField] private TMP_Text _textTotalStarted;
    [SerializeField] private TMP_Text _textTotalTime;
    [SerializeField] private WindowTimer _windowTimer;
    [SerializeField] private RectTransform _textContainer;

    private const string TitleTime = "Время игры составило: ";
    private const string TitleBest = "Рекорд по времени: ";
    private const string TitleAverage = "Среднее время: ";
    private const string TitleCountLoss = "Количество проигрышей: ";
    private const string TitleCountWin = "Количество выигрышей: ";
    private const string TitleTotalStarted = "Начато игр: ";
    private const string TitleTotalTime = "Всего наиграно времени: ";
    private const float OffsetXSpace = 0.1f;
    private ConverterTime _converterTime;
    private CustomizerWindow _customizerWindow;

    private void Awake()
    {
        Hide();
        _converterTime = new ConverterTime();
        _customizerWindow = new CustomizerWindow();
    }

    private void Start()
    {
        _customizerWindow.InitSizeWindow(transform);
    }

    public void Display(WindowTimer windowTimer)
    {
        var gameField = _gameState.UIData.GameField;
        var dataSetups = gameField.DataSetting.StatisticsData.DataSetups;
        Open();
        transform.SetAsLastSibling();
        _buttonPlay.Enable();
        _textTimeResult.text = TitleTime + _converterTime.GetTimeToString(_windowTimer.GetTimeIntResult());
        _textRecordTimeResult.text = TitleBest + _converterTime.GetTimeToString(dataSetups.BestPlayGameSecondsTime);
        _textAverageTimeResult.text = TitleAverage + _converterTime.GetTimeToString(dataSetups.AverageGameSecondsTime);
        _textCountLoss.text = TitleCountLoss + dataSetups.CountLoss;
        _textCountWins.text = TitleCountWin + dataSetups.CountWins;
        _textTotalStarted.text = TitleTotalStarted + dataSetups.TotalGameStarted;
        _textTotalTime.text = TitleTotalTime + _converterTime.GetTimeToString(dataSetups.TotalPlayGamesSeconds);
        
        var anchorMin = Vector2.zero;
        var anchorMax = Vector2.one;

        anchorMin.x = OffsetXSpace;
        anchorMax.x = 1 - OffsetXSpace;
        _textContainer.anchorMin = anchorMin;
        _textContainer.anchorMax = anchorMax;
        _textContainer.offsetMin = Vector2.zero;
        _textContainer.offsetMax = Vector2.zero;
    }

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand) => Hide();
}