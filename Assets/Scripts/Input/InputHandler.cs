using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;


public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _delayClickTime;
    private CellView _cellView;
    private float _startClickTime;
    private bool _isButtonPressed = false;
    private Button _button;
    public CellView CellView => _cellView;
    public event Action<InputHandler> OnActivateCell;
    public event Action<InputHandler> OnActivateFlag;

    private void Awake()
    {
        _cellView = GetComponent<CellView>();
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
        if (AllowTimeHoldForDuration() == false) return;
        OnActivateCell?.Invoke(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startClickTime = Time.time;
        _isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonPressed = false;
    }

    public bool AllowTimeHoldForDuration() => ((Time.time - _startClickTime) <= _delayClickTime);

    private void Update()
    {
        var inputMouse0 = Input.GetMouseButton(0);

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            var inputMouse1 = Input.GetMouseButton(1);

            if (inputMouse0 && _isButtonPressed)
            {
                OnActivateCell?.Invoke(this);
                _isButtonPressed = false;
            }
            else if (inputMouse1 && _isButtonPressed)
            {
                OnActivateFlag?.Invoke(this);
                _isButtonPressed = false;
            }
        }

        else if (inputMouse0 == true && _isButtonPressed == true && (AllowTimeHoldForDuration() == false))
        {
            OnActivateFlag?.Invoke(this);
            _isButtonPressed = false;
        }
    }
}