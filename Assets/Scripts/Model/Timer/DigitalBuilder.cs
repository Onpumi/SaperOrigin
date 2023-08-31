using System;
using UnityEngine;

public class DigitalBuilder
{
    private DigitalView[] _digitalViews;
    private Sprite[] _sprites;

    public DigitalBuilder(Sprite[] sprites, DigitalView[] digitalViews)
    {
        _digitalViews = digitalViews;
        _sprites = sprites;
    }

    public void Display(string stringTime)
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
    
    
    public void Display(int countMines)
    {
        var stringCount = countMines.ToString();

        if (countMines < 10 && countMines >= 0)
        {
            for (int i = 0; i < _digitalViews.Length; i++)
            {
                if( i == _digitalViews.Length - 1 ) _digitalViews[i].Display(_sprites[countMines]);
                else
                {
                    _digitalViews[i].Display(_sprites[0]);
                }
            }

            return;
        }
        
        for (int i = 0; i < stringCount.Length; i++)
        {
            if (int.TryParse(stringCount[i].ToString(), out int index) == true)
            {
                if (index >= 0 && index < 10)
                    _digitalViews[i].Display(_sprites[index]);
            }
        }
    }
    
    
    
    
}