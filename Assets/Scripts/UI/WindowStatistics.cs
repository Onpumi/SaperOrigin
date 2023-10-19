
using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowStatistics : WindowBase
{
    [SerializeField] private List<UIInpurtCheckStatistic> _checkStatistics;
    private CustomizerWindow _customizerWindow;

    private void Awake()
    {
        _customizerWindow = new CustomizerWindow();
        Hide();
        
    }

    private void Start()
    {
        _customizerWindow.InitSizeWindow(transform.GetChild(0), 0.7f);
    }

    public override void Enable()
    {
        base.Enable();
        UpdateStatusesMenu();
    }

    private void UpdateStatusesMenu()
    {
        foreach (var checkStatistic in _checkStatistics)
        {
            checkStatistic.ChangeDifficult();
        }
    }
    
    
}