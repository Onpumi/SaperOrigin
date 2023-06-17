using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowTimer : WindowBase
{
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _deltaTime = 1f;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private BorderField _borderField; 
    private DateTime _time = DateTime.Today;
    private StringBuilder _stringBuilder;
    public string TimeValue => _tmpText.text;
    private bool _isPause = false;
    private float _seconds;
    private DigitalView[] _digitalViews;
    private TimeBuilder _timeBuilder;
    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;


    private void Start()
    {
        _stringBuilder = new StringBuilder();
        _digitalViews = new DigitalView[transform.childCount];
        int count = 0;
        foreach (Transform view in transform)
        {
            _digitalViews[count++] = view.GetComponent<DigitalView>();
        }
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
       
        _timeBuilder = new TimeBuilder(_sprites, _digitalViews);
    }

    public void StartTimer()
    {
        ResetValue();
        _seconds = 0;
        var RectBorder = _borderField.RectTransform.rect;
        var widthCell = (_rectTransform.rect.width ) / _digitalViews.Length;
        var heightCell = _rectTransform.rect.height;
        _gridLayoutGroup.cellSize = new Vector2(widthCell, heightCell);
        InvokeRepeating(nameof(UpdateTimer), 0, _deltaTime);
        _borderField.Init(_rectTransform);
    }

    public void ResetValue()
    {
        var text = DateTime.Today.ToString("mm:ss");
        _stringBuilder.Append(text);
        
        _time = DateTime.Today;
        _seconds = 0;
    }

    private int count = 0;

    private void UpdateTimer()
    {
        if (_isPause == false)
        {
            _seconds++;
            _time = DateTime.Today.AddSeconds(_seconds);
            _timeBuilder.DisplayDigitals( _time.ToString("mm:ss"));
        }
    }

    public void StopTimer()
    {
        ResetValue();
        CancelInvoke(nameof(UpdateTimer));
    }

    public void PauseTime(bool value) => _isPause = value;

    public void Display(float elapsedTime)
    {
        Debug.Log(elapsedTime);
    }

    private void Update()
    {
    }
}