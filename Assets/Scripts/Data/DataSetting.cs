using UnityEngine;

public class DataSetting
{
    private GameField _gameField;
    public AudioData AudioData { get; private set; }
    public ScreenData ScreenData { get; private set; }
    public GameData GameData { get; private set; }
    public StatisticsData StatisticsData { get; private set; }

    public DataSetting(GameField gameField)
    {
        _gameField = gameField;
        AudioData = new AudioData("AudioKey");
        AudioData.Load();
        ScreenData = new ScreenData("ScreenKey");
        ScreenData.Load();
        GameData = new GameData("GameKey", gameField);
        GameData.Load();
        UpdateStatisticsData(gameField);
        InitScreen();
    }

    public void UpdateStatisticsData( GameField gameField )
    {
        StatisticsData = new StatisticsData("StatisticsKey" + gameField.DifficultLevel);
        StatisticsData.Load();
    }
    

    private void InitScreen()
    {
        Screen.fullScreenMode = (ScreenData.GetValue(TypesScreen.ScreenFullOn))
            ? (FullScreenMode.MaximizedWindow)
            : (FullScreenMode.Windowed);
        Screen.sleepTimeout = (ScreenData.GetValue(TypesScreen.ScreenSleepTimeOutOn))
            ? (SleepTimeout.SystemSetting)
            : (SleepTimeout.NeverSleep);
    }

    public void SetScreen(TypesScreen typesScreen, bool value)
    {
        if (typesScreen == TypesScreen.ScreenFullOn)
        {
            Screen.fullScreenMode = (value) ? (FullScreenMode.MaximizedWindow) : (FullScreenMode.Windowed);
        }
        else if (typesScreen == TypesScreen.ScreenSleepTimeOutOn)
        {
            Screen.sleepTimeout = (value) ? (SleepTimeout.SystemSetting) : (SleepTimeout.NeverSleep);
        }
    }
  
}