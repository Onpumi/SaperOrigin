using System;
using UnityEngine;
using TMPro;

public class WindowCountMines : WindowBase
{
    [SerializeField] private TMP_Text _tmpText;
    
    private void Start()
    {
        gameObject.SetActive(false);
        _tmpText.text = "0";
    }

    public void Display( int countMines )
    {
        _tmpText.text =  Convert.ToString( countMines );
    }
    
}
