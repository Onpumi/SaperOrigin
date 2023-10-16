using System;
using UnityEngine;
using TMPro;

public class WindowWinner : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private IWindowCommand _buttonPlay;
    [SerializeField] private TMP_Text _textTimeResult;
    [SerializeField] private TMP_Text _textRecordTimeResult;
    [SerializeField] private TMP_Text _textAverageTimeResult;
    [SerializeField] private WindowTimer _windowTimer;
    private const string TitleTime = "Время игры составило: ";
    private const string TitleBest = "Рекорд по времени: ";
    private const string TitleAverage = "Средняя продолжительность: ";
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
        Open();
        transform.SetAsLastSibling();
        _buttonPlay.Enable();
        _textTimeResult.text = TitleTime + _converterTime.GetTimeToString(_windowTimer.GetTimeIntResult());
        _textRecordTimeResult.text =
            TitleBest + _converterTime.GetTimeToString(gameField.DataSetting.StatisticsData.DataSetups
                .BestPlayGameSecondsTime);
        _textAverageTimeResult.text = TitleAverage +
                                      _converterTime.GetTimeToString(gameField.DataSetting.StatisticsData.DataSetups
                                          .AverageGameSecondsTime);
    }

    public override void OpenCanvasByPressingEscape(IWindowCommand windowCommand) => Hide();
}