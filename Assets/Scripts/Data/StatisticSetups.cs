
using UnityEngine;

public class StatisticSetups
{
    public int CountWins;
    public int CountLoss;
    public int AverageGameSecondsTime;
    public int BestPlayGameSecondsTime;
    public int TotalGameStarted;
    public int TotalPlayGamesSeconds;
    private int _countPlayes;

    public StatisticSetups()
    {
        CountWins = 0;
        CountLoss = 0;
        AverageGameSecondsTime = 0;
        TotalGameStarted = 0;
        BestPlayGameSecondsTime = -1;
        TotalPlayGamesSeconds = 0;
        _countPlayes = 0;
    }

    public void UpdateCountFinishPlayGames(bool isWin)
    {
        if (isWin)
            CountWins++;
        else
            CountLoss++;
        _countPlayes++;
    }

    public void CalculateAverageTime()
    {
        AverageGameSecondsTime =  (int)((float)TotalPlayGamesSeconds / (float)_countPlayes);
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

    public int GetCountStartPlayGames()
    {
        return TotalGameStarted;
    }
}