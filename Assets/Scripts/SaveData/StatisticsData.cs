using SaveData;

public class StatisticsData : SavingData<StatisticSetups>
{
    public StatisticsData( string key )
    {
        base.Key = key;
    }

    public void UpdateTotalGameStarted()
    {
        DataSetups.UpdateTotalGameStarted(); 
        Save();
    }

    public void UpdateTotalPlayGamesSeconds(int seconds)
    {
        DataSetups.UpdateTotalPlayGamesSeconds(seconds);
        Save();
    }

    public void UpdateCountFinishPlayGames( bool isWin )
    {
        DataSetups.UpdateCountFinishPlayGames( isWin );
        Save();
    }

    public void SetupBestPlayGames( int seconds )
    {
        DataSetups.SetupBestPlayGames(seconds);
        Save();
    }
    
    

    public void CalculateAverageTime()
    {
        DataSetups.CalculateAverageTime();
        Save();
    }

    public int GetCountStartPlayGames() => DataSetups.GetCountStartPlayGames();


}