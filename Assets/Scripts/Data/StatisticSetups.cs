using UnityEngine;

public class StatisticSetups
{
    public int CountWins;
    public int CountLoss;
    public int CountPlayes;
    public int AverageGameSecondsTime;
    public int BestPlayGameSecondsTime;
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
    }

    public void CalculateAverageTime()
    {
        AverageGameSecondsTime = TotalPlayGamesSeconds / CountPlayes;
        Debug.Log(AverageGameSecondsTime);
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