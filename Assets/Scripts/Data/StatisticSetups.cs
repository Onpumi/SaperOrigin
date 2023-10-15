
public class StatisticSetups
{
    public int CountWins;
    public int CountLoss;
    public int CountPlayes;
    public int AverageGameSecondsTime;
    public int BestPlayGameSecondsTime { get; private set; }
    public int TotalGameStarted;
    public int TotalPlayGamesSeconds;

    public StatisticSetups()
    {
        CountWins = 0;
        CountLoss = 0;
        AverageGameSecondsTime = 0;
        TotalGameStarted = 0;
        BestPlayGameSecondsTime = -1;
        TotalPlayGamesSeconds = 0;
        CountPlayes = 0;
    }

    public void UpdateCountFinishPlayGames(bool isWin)
    {
        if (isWin)
            CountWins++;
        else
            CountLoss++;
        
        CountPlayes++;
        
//        Debug.Log( "количество проигрышей:" + CountLoss);
//        Debug.Log( "количество выигрышей:" + CountWins);
        
    }

    public void CalculateAverageTime()
    {
        AverageGameSecondsTime =  (int)((float)TotalPlayGamesSeconds / (float)CountPlayes);
    }

    public void UpdateTotalPlayGamesSeconds(int seconds)
    {
        if (seconds > 0)
            TotalPlayGamesSeconds += seconds;
    }

    public void SetupBestPlayGames(int seconds)
    {
        if (BestPlayGameSecondsTime == -1)
        {
            BestPlayGameSecondsTime = seconds;
        }
        else
        {
            if (seconds < BestPlayGameSecondsTime)
            {
                BestPlayGameSecondsTime = seconds;
            }
        }
        
        //Debug.Log(BestPlayGameSecondsTime);
        
    }

    public void UpdateTotalGameStarted()
    {
        TotalGameStarted++;
    }

    public int GetCountPlayGames(bool isWin)
    {
        if (isWin)
            return CountWins;
        else
            return CountLoss;
    }

    public (int, int) GetPlayTimesValue()
    {
        return (AverageGameSecondsTime, BestPlayGameSecondsTime);
    }

    public int GetCountStartPlayGames()
    {
        return TotalGameStarted;
    }
}