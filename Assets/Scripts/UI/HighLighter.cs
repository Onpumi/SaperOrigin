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
    private UIInputCheckSound _uiInputCheckSound;
    private UIInputCheckGame _uiInputCheckGame;
    private UIInputCheckScreen _uiInputCheckScreen;

    private void Awake()
    {

        if (SystemInfo.operatingSystemFamily != OperatingSystemFamily.Windows)
        {
            enabled = false;
        }
        
        
        _textMenu = GetComponentInChildren<TMP_Text>();
        _uiCheckButton = GetComponentInChildren<UICheckButton>();
        if (_uiCheckButton is not null)
            _image = _uiCheckButton.GetComponent<Image>();

        _uiInputCheckGame = GetComponent<UIInputCheckGame>();

        if (_uiInputCheckGame is null) _image ??= GetComponent<Image>();

        _uiInputCheckComplexity = GetComponent<UIInputCheckComplexity>();
        _uiInputCheckSound = GetComponent<UIInputCheckSound>();
        _uiInputCheckScreen = GetComponent<UIInputCheckScreen>();
        
        
        
    }

    private void SetImageColor(Color color)
    {
        if (_image is not null) _image.color = color;
    }

    private void SetTextColor(Color color)
    {
        if (_textMenu is not null) _textMenu.color = color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_textMenu is not null)
            _prevColor = _textMenu.color;
        if (_image is not null)
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

        if (_uiInputCheckSound is not null || _uiInputCheckScreen is not null || _uiInputCheckGame is not null)
        {
            return;
        }


        if (_image is not null)
            _prevColor = _image.color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_uiInputCheckComplexity is not null)
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