
using UnityEngine;

public class WindowStatistics : WindowBase
{
    private RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    public void Display()
    {
        Enable();
    }


    private void SetSizeWindow()
    {
        
    }
    
    
    
    
}
