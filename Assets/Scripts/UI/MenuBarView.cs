using System;
using UnityEngine;

public class MenuBarView : WindowBase
{
    [SerializeField] private MenuType _menuType;
    private float _anchorMinY;
    private Rect SafeArea => Screen.safeArea;
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        if( _menuType == MenuType.Top )
         SetPropertiesTopMenu();
        else if (_menuType == MenuType.Bottom)
        {
            SetPropertiesBottomMenu();        
        }
    }
    private void SetPropertiesTopMenu()
    {
        var anchorMin = SafeArea.position;
        var anchorMax = anchorMin + SafeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        _anchorMinY = anchorMax.y;
        RectTransform.anchorMin = new Vector2(anchorMin.x, _anchorMinY);
        RectTransform.anchorMax = anchorMax;
    }

    private void SetPropertiesBottomMenu()
    {
        RectTransform.pivot = Vector2.one * 0.5f;
        RectTransform.offsetMin = Vector2.zero;
        RectTransform.offsetMax = Vector2.zero;

        var anchorMin = new Vector2( 0f, 0f);
        //var anchorMax = new Vector2( 1f, 15f / Screen.height);
        var anchorMax = new Vector2( 1f, 0.02f);
        
        RectTransform.anchorMin = anchorMin;
        RectTransform.anchorMax = anchorMax;
    }
}

public enum MenuType
{
    Top,
    Bottom,
    Left,
    Right,
    Center
}