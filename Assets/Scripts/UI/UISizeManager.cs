using System;
using UnityEngine;
using TMPro;

public class UISizeManager : MonoBehaviour
{
    [SerializeField] private float _scaleWidth = 1f;
    [SerializeField] private MenuBarView _topMenu;


    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (_topMenu != null)
        {
            _topMenu.FitSizeUI(rectTransform, _scaleWidth);
        }
    }

    public void SetSize(RectTransform parentRectTransform)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta =
            new Vector2(parentRectTransform.rect.height * _scaleWidth, parentRectTransform.rect.height);
        var tmpText = transform.GetComponentInChildren<TMP_Text>();



        if (tmpText != null)
        {
            var rectTransformChild = tmpText.GetComponent<RectTransform>();
            if (rectTransformChild != null)
            {
                rectTransformChild.sizeDelta = new Vector2(parentRectTransform.rect.height * _scaleWidth,
                    parentRectTransform.rect.height);
            }
        }
    }
}