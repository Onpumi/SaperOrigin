
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DigitalView : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        transform.localScale = Vector2.one * 0.8f;
    }

    public void Display( Sprite sprite )
    {
        _image.sprite = sprite;
    }

}