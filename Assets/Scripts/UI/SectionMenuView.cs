using System;
using UnityEngine;

public class SectionMenuView : MonoBehaviour
{
    [SerializeField] private MenuType MenuType;
    [SerializeField] private RectTransform _centerImage;
    [SerializeField] private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>() ??
                         throw new ArgumentException("RectTransform in SectionMenu is null");
        if (MenuType != MenuType.Center)
        {
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }

        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        SetProperties();
    }

    private void SetProperties()
    {
        var parentRect = _parentRectTransform.rect;
        var heightParent = parentRect.height;
        
        if (MenuType == MenuType.Left)
        {
            _rectTransform.anchorMin = new Vector2(0f, 0f);
            _rectTransform.anchorMax = new Vector2(0.5f, 1f);
            _rectTransform.offsetMax = new Vector2(-heightParent/2f,0f);
        }
        else if (MenuType == MenuType.Right)
        {
            _rectTransform.anchorMin = new Vector2(0.5f, 0f);
            _rectTransform.anchorMax = new Vector2(1f, 1f);
            _rectTransform.offsetMin = new Vector2(heightParent/2f,0f);
        }
        else if (MenuType == MenuType.Center)
        {
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.sizeDelta = new Vector2(heightParent, heightParent);
        }
    }
}