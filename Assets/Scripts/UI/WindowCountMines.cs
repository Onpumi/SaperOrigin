using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindowCountMines : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private MenuBarView _topMenu;
    private DigitalView[] _digitalViews;
    private GridLayoutGroup _gridLayoutGroup;

    private void Start()
    {
        gameObject.SetActive(false);
        _digitalViews = new DigitalView[transform.childCount];
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        int count = 0;
        foreach (Transform view in transform)
        {
            _digitalViews[count] = view.GetComponent<DigitalView>();
            count++;
        }

        InitSizeFieldTime();
    }


    public void ResetValue( int count )
    {
        Display(count);
    }

    private void InitSizeFieldTime()
    {
        
        var widthCell = _topMenu.Height / 2f;
        var heightCell = _topMenu.Height;
        _gridLayoutGroup.cellSize = new Vector2(widthCell, heightCell);
    }
    

    public void Display(int countMines)
    {
        gameObject.SetActive(true);
        var stringCount = countMines.ToString();

        if (countMines < 10 && countMines >= 0)
        {
            for (int i = 0; i < _digitalViews.Length; i++)
            {
                if( i == _digitalViews.Length - 1 ) _digitalViews[i].Display(_sprites[countMines]);
                else _digitalViews[i].Display(_sprites[0]);
            }
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