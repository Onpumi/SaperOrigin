using System;
using UnityEngine;

public class SectionMenuView : MonoBehaviour
{
    [SerializeField] private MenuType MenuType;
    [SerializeField] private RectTransform _centerImage;
    [SerializeField] private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;
    


    private void Awake()
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
        var widthCenterImage = (_centerImage != null) ? (_centerImage.rect.width) : (0);
        var widthPercentCenterImage = (widthCenterImage * 0.5f) / _parentRectTransform.rect.width;
        if (MenuType == MenuType.Left)
        {
            _rectTransform.anchorMin = new Vector2(0f, 0f);
            _rectTransform.anchorMax = new Vector2(0.5f - widthPercentCenterImage, 1f);
        }
        else if (MenuType == MenuType.Right)
        {
            _rectTransform.anchorMin = new Vector2(0.5f + widthPercentCenterImage, 0f);
            _rectTransform.anchorMax = new Vector2(1f, 1f);
        }
        else if (MenuType == MenuType.Center)
        {

            _rectTransform.anchorMin = new Vector2(0.45f, 0f);
            _rectTransform.anchorMax = new Vector2(0.55f, 1f);
            _rectTransform.sizeDelta = new Vector2(0f, 0f);
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
             _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}