using SaveData;
using UnityEngine;

public class StatisticsData : SavingData<StatisticSetups>
{
    public StatisticsData( string key )
    {
        base.Key = key;
    }


    public void UpdateCountStart()
    {
        DataSetups.UpdateTotalGameStarted(); 
        Save();
    }

    public void UpdateTotalPlayGamesSeconds(int seconds)
    {
        DataSetups.UpdateTotalPlayGamesSeconds(seconds);
        Save();
    }
    
    
    public void SetupValue( bool value )
    {
        DataSetups.UpdateCountFinishPlayGames( value );
        DataSetups.CalculateAverageTime();
        Save();
    }

    public (int,int) GetTimesValue()
    {
        return DataSetups.GetPlayTimesValue();
    }

    public int GetCountPlayGames( bool isWin )
    {
        return DataSetups.GetCountPlayGames( isWin );
    }

    public void UpdateCountFinishPlayGames( bool isWin )
    {
        DataSetups.UpdateCountFinishPlayGames( isWin );
        Save();
    }

    public void CalculateAverageTime()
    {
        DataSetups.CalculateAverageTime();
    }

    public int GetCountStartPlayGames() => DataSetups.GetCountStartPlayGames();


}