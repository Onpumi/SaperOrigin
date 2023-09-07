using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HighLighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text _textMenu;
    private UICheckButton _uiCheckButton;
    private Color _prevColor;
    private Image _image;

    private void Awake()
    {
        _textMenu = GetComponentInChildren<TMP_Text>();
        _uiCheckButton = GetComponentInChildren<UICheckButton>();
        if (_uiCheckButton != null)
            _image = _uiCheckButton.GetComponent<Image>();
    }

    private void SetImageColor(Color color)
    {
        if (_image != null) _image.color = color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _prevColor = _textMenu.color;
        _textMenu.color = Color.cyan;
        SetImageColor(Color.cyan);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textMenu.color = _prevColor;
        SetImageColor(_prevColor);
    }
}