using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverterTime
{
    public string GetTimeToString(int seconds)
    {
        int totalMinutes = seconds / 60;
        int totalSeconds = seconds % 60;
        string TitleMinutes;
        string TitleSeconds;

        string result = "";

        if (totalMinutes == 0)
            TitleMinutes = "";
        else
            TitleMinutes = totalMinutes + " мин. ";

        if (totalSeconds == 0)
            TitleSeconds = "";
        else
            TitleSeconds = totalSeconds + " сек. ";



        return TitleMinutes + TitleSeconds;
    }
}