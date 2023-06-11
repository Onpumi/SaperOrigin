
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerButtonMode : WindowBase, IPointerDownHandler
{
    [SerializeField] private RectTransform _rectTransformPanel;
    [SerializeField] private Transform _uiFlag;
    [SerializeField] private Transform _uiMine;
    private Button _button;
    public ButtonMode Mode { get; private set; }
    
    private void Awake()
    {
        Mode = ButtonMode.Mine;
        Display();
        _uiMine.transform.SetAsLastSibling();
        _button = GetComponent<Button>();
        
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetModePlay);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SetModePlay);
    }

    public void OnPointerDown( PointerEventData eventData )
    {
    }
    
    private void Display()
    {
        if ( Mode == ButtonMode.Flag )
        {
            ExchangeScaleUI(_uiFlag, _uiMine);
        }
        else if ( Mode == ButtonMode.Mine )
        {
            ExchangeScaleUI(_uiMine, _uiFlag);
        }
    }

    private void SetModePlay()
    {
        Mode = (Mode == ButtonMode.Mine) ? (ButtonMode.Flag) : (ButtonMode.Mine);
        ReplacingIndexesUI(_uiFlag.transform, _uiMine.transform );
        Display();
    }

    private void ReplacingIndexesUI( Transform transform1, Transform transform2)
    {
        var bufferIndex = transform2.GetSiblingIndex();
        transform2.SetSiblingIndex(transform1.GetSiblingIndex());
        transform1.SetSiblingIndex(bufferIndex);
    }

    private void ExchangeScaleUI( Transform transform1, Transform transform2 )
    {
        SetScaleUI( transform1, RectTransform.Edge.Left, RectTransform.Edge.Bottom, 1f );
        //SetScaleUI( transform2, RectTransform.Edge.Right, RectTransform.Edge.Top, 0.5f );
        transform1.gameObject.SetActive(true);
        transform2.gameObject.SetActive(false);
    }

    private void SetScaleUI( Transform transformUI, RectTransform.Edge edge1, RectTransform.Edge edge2, float scale)
    {
        Image image = transformUI.GetComponent<Image>();
        RectTransform rectTransform = transformUI.GetComponent<RectTransform>();
        rectTransform.SetInsetAndSizeFromParentEdge(edge1, 0,   _rectTransformPanel.sizeDelta.y * scale);
        rectTransform.SetInsetAndSizeFromParentEdge(edge2, 0, _rectTransformPanel.sizeDelta.y * scale);

        if (_rectTransformPanel != null)
        {
            rectTransform.sizeDelta = new Vector2(_rectTransformPanel.sizeDelta.y * scale, _rectTransformPanel.sizeDelta.y * scale);
        }
    }

    
    
    
}

public enum ButtonMode
{
    Mine,
    Flag
}