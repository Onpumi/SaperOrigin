using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WindowTimer : WindowBase
{
    [SerializeField] private float _deltaTime = 1f;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private BorderField _borderField;
    [SerializeField] private MenuBarView _topMenu;
    private DateTime _time = DateTime.Today;
    private StringBuilder _timeFinish;
    private bool _isPause = false;
    private float _seconds;
    private DigitalView[] _digitalViews;
    private DigitalBuilder _digitalBuilder;
    private GridLayoutGroup _gridLayoutGroup;
    private float _widthCell;
    private float _heightCell;
    


    private void Start()
    {
        _timeFinish = new StringBuilder();
        _digitalViews = new DigitalView[transform.childCount];
        int count = 0;
        foreach (Transform view in transform)
        {
            _digitalViews[count] = view.GetComponent<DigitalView>();
            count++;
        }
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _digitalBuilder = new DigitalBuilder(_sprites, _digitalViews);
        InitSizeFieldTime();
        transform.localScale = Vector2.one * 0.8f;
    }


    public void InitSizeFieldTime()
    {
        _widthCell = _topMenu.Height / 3f;
        _heightCell = _topMenu.Height;
       _gridLayoutGroup.cellSize = new Vector2(_widthCell, _heightCell);
    }
    

    public void StartTimer()
    {
        _timeFinish.Clear();
        ResetValue();
        _seconds = 0;
        InvokeRepeating(nameof(UpdateTimer), 0, _deltaTime);
    }

    public void ResetValue()
    {
        _time = DateTime.Today;
        _digitalBuilder.Display( _time.ToString("mm:ss"));
        _seconds = 0;
    }

    private void UpdateTimer()
    {
        if (_isPause == false)
        {
            _seconds++;
            _time = DateTime.Today.AddSeconds(_seconds);
            _digitalBuilder.Display( _time.ToString("mm:ss"));
        }
    }

    public float GetTotalSeconds()
    {
        return _seconds;
    }

    public void StopTimer()
    {
        _timeFinish.Append(_time.ToString("mm:ss"));
        CancelInvoke(nameof(UpdateTimer));
    }

    public string GetTimeResult() => _timeFinish.ToString();

    public void PauseTime(bool value) => _isPause = value;

}