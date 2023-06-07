using System;
using UnityEngine;

public class WindowPanelView : WindowBase
{
    private float _anchorMinY;
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        SetSafeAreaForRecTransform();
        //SetScaleChildElements();
    }

    private void SetScaleChildElements()
    {
        foreach (Transform child in transform)
        {
            var uiSizeManager = child.GetComponent<UISizeManager>() ??
                                throw new ArgumentException("UISizeManager is null");
                //  uiSizeManager.SetSize(RectTransform);
        }
    }

    private void SetSafeAreaForRecTransform()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        _anchorMinY = anchorMax.y;
        RectTransform.anchorMin = new Vector2(anchorMin.x, _anchorMinY);
        RectTransform.anchorMax = anchorMax;
    }
}