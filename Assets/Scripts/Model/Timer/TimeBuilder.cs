using System;
using System.Text;
using UnityEngine;

public class TimeBuilder
{
    private DigitalView[] _digitalViews;
    private Sprite[] _sprites;

    public TimeBuilder(Sprite[] sprites, DigitalView[] digitalViews)
    {
        _digitalViews = digitalViews;
        _sprites = sprites;
    }

    public void DisplayDigitals(string stringTime)
    {
        if (_digitalViews.Length != stringTime.Length)
        {
            throw new ArgumentException("DigitalView length must be exactly the string time length!");
        }

        var indexCenterString = 10;

        for (int i = 0; i < stringTime.Length; i++)
        {
            if (int.TryParse(stringTime[i].ToString(), out int index) == false)
            {
                _digitalViews[i].Display(_sprites[indexCenterString]);
                continue;
            }

            if ( index >= 0 && index < 10 )
                _digitalViews[i].Display(_sprites[index]);
        }
    }
}