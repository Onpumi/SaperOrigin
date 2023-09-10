using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HighLighter : WindowBase, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private WindowSettings _windowSettings;
    private TMP_Text _textMenu;
    private UICheckButton _uiCheckButton;
    private Color _prevColor;
    private Image _image;
    private UIInputCheckComplexity _uiInputCheckComplexity;

    private void Awake()
    {
        _textMenu = GetComponentInChildren<TMP_Text>();
        _uiCheckButton = GetComponentInChildren<UICheckButton>();
        if (_uiCheckButton != null)
            _image = _uiCheckButton.GetComponent<Image>();
        if (_image == null) _image = GetComponent<Image>();

        _uiInputCheckComplexity = GetComponent<UIInputCheckComplexity>();
    }

    private void SetImageColor(Color color)
    {
        if (_image != null) _image.color = color;
    }

    private void SetTextColor(Color color)
    {
        if (_textMenu != null) _textMenu.color = color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_textMenu != null)
            _prevColor = _textMenu.color;
        if (_image != null)
            _prevColor = _image.color;
        var color = _windowSettings.ColorHighLightMenu;
        SetTextColor(color);
        SetImageColor(color);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_uiInputCheckComplexity != null)
        {
            _uiInputCheckComplexity.SetActive(true);
        }

        if (_image != null)
            _prevColor = _image.color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_uiInputCheckComplexity != null)
        {
            _uiInputCheckComplexity.UpdateValue();
        }
        else
        {
            SetImageColor(_prevColor);
            SetTextColor(_prevColor);
        }
    }
}