using UnityEngine;

public class WindowStatistics : WindowBase
{
    [SerializeField] private RectTransform _buttonField;
    [SerializeField] private RectTransform _buttonBack;
    private RectTransform _rectTransform;
    private Vector2 _anchorMin = (Screen.width < Screen.height) ? (new Vector2(0, 0.1f)) : (new Vector2(0.25f, 0.05f));
    private Vector2 _anchorMax = (Screen.width < Screen.height) ? (new Vector2(1, 0.3f)) : (new Vector2(0.75f, 0.2f));

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Start()
    {
        SetSizeWindow();
    }

    public void Display()
    {
        Enable();
    }


    private void SetSizeWindow()
    {
        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        InitSizeButton();
    }


    private void InitSizeButton()
    {
            _buttonField.offsetMin = Vector2.zero;
            _buttonField.offsetMax = Vector2.zero;
            _buttonField.anchorMin = _anchorMin;
            _buttonField.anchorMax = _anchorMax;
            _buttonBack.offsetMin = Vector2.zero;
            _buttonBack.offsetMax = Vector2.zero;
            _buttonBack.anchorMin = Vector2.one * 0.5f;
            _buttonBack.anchorMax = Vector2.one * 0.5f;
            _buttonBack.sizeDelta = new Vector2(_buttonField.rect.width * 0.5f, _buttonField.rect.height * 0.5f);
    }
}