using System;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
public class WindowTimer : WindowBase
{
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _deltaTime = 1f;
    private DateTime _time = DateTime.Today;
    public string TimeValue => _tmpText.text;
    private bool _isPause = false;
    private float _seconds; 
    public void StartTimer()
    {
        ResetValue();
        _seconds = 0;
        InvokeRepeating( nameof(UpdateTimer), 0, _deltaTime );
        //InvokeRepeating(() => { Debug.Log(" ");}, 0, _deltaTime );
        
    
    }

    public void ResetValue()
    {
          _tmpText.text = DateTime.Today.ToString("mm:ss");
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
            _tmpText.text = _time.ToString("mm:ss");
        }
    }

    public void StopTimer()
    {
        ResetValue();
        CancelInvoke(nameof(UpdateTimer));
    }

    public void PauseTime( bool value ) => _isPause = value;

    public void Display(float elapsedTime)
    {
        Debug.Log(elapsedTime);
    }

    private void Update()
    {
        
    }



}


