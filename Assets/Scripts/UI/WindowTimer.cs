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
    private StringBuilder _timeFinish;
    private bool _isPause = false;
    private float _seconds;
    private DigitalView[] _digitalViews;
    private TimeBuilder _timeBuilder;
    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;
    private float _widthCell;
    private float _heightCell;


    private void Start()
    {
        _timeFinish = new StringBuilder();
        _digitalViews = new DigitalView[transform.childCount];
        int count = 0;
        foreach (Transform view in transform)
        {
            _digitalViews[count++] = view.GetComponent<DigitalView>();
        }
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _timeBuilder = new TimeBuilder(_sprites, _digitalViews);
        InitSizeFieldTime();
    }


    private void InitSizeFieldTime()
    {
        // _widthCell = (_rectTransform.rect.width ) / _digitalViews.Length;
        _widthCell = 50f;
        _heightCell = _rectTransform.rect.height;
       _gridLayoutGroup.cellSize = new Vector2(_widthCell, _heightCell);
     
    }

    public void StartTimer()
    {
        _timeFinish.Clear();
        ResetValue();
        _seconds = 0;
        InitSizeFieldTime();
        InvokeRepeating(nameof(UpdateTimer), 0, _deltaTime);
    }

    public void ResetValue()
    {
        _time = DateTime.Today;
        _timeBuilder.DisplayDigitals( _time.ToString("mm:ss"));
        _seconds = 0;
    }

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
        _timeFinish.Append(_time.ToString("mm:ss"));
        ResetValue();
        CancelInvoke(nameof(UpdateTimer));
    }

    public string GetTimeResult() => _timeFinish.ToString();

    public void PauseTime(bool value) => _isPause = value;

  

}