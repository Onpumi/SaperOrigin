using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _delayClickTime;
    [SerializeField] private CellView _cellView;
    private float _startClickTime;
    private bool _isClick = false;
    private Button _button;
    public CellView CellView => _cellView;
    public event Action<InputHandler> OnClickCell;
    public event Action<InputHandler> OnClickDelay;
    


    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ActivateClickBlock);
        
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ActivateClickBlock);
    }

    private void ActivateClickBlock()
    {
        if (IsTimeShort() == false) return;
        OnClickCell?.Invoke(this);
    }


  

    public void OnPointerDown(PointerEventData eventData)
    {
        _startClickTime = Time.time;
        _isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if( _isClick == true )
            OnClickCell?.Invoke(this);
        _isClick = false;
            
    }

    public bool IsTimeShort() => ((Time.time - _startClickTime) <= _delayClickTime);

    private void Update()
    {
        if (Input.GetMouseButton(0) == true && _isClick == true && (IsTimeShort() == false) )
        {
            OnClickDelay?.Invoke(this);
            _isClick = false;
        }
    }
    
    
}