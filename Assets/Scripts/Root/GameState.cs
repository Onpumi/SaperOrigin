using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Threading.Tasks;
using YG;

[RequireComponent(typeof(UIData))]
public class GameState : SerializedMonoBehaviour, ICompositeRoot
{
    [SerializeField] private List<IWindowCommand> _ui;
    [SerializeField] private Vibration _vibration;
    [SerializeField] private Views _views;
    private UIData _uiData;
    private TimerPlayer _timerPlayer;
    public GameFieldData GameFieldData { get; private set; }
    public List<IWindowCommand> UI => _ui;
    public UIData UIData => _uiData;
    public bool IsPlay { get; private set; }
    public bool IsPause { get; private set; }
    private List<IBackToPreviousWindowCommand> _backToPreviousWindowCommand;
    public IBackPreviousWindow BackPreviousWindow { get; private set; }

    public IWindowCommand CurrentWindow { get; private set; }
    public ClassForJavaScript JS;
    public Views Views => _views;


    public void Init()
    {
        GameFieldData = ScriptableObject.CreateInstance<GameFieldData>();
        _ui ??= new List<IWindowCommand>();
        _uiData ??= GetComponent<UIData>();
        _timerPlayer = new TimerPlayer(_uiData.WindowTimer);
        IsPlay = false;
        _backToPreviousWindowCommand = new List<IBackToPreviousWindowCommand>();
        BackPreviousWindow = new BackOpenerWindow();
    }


    public void Vibrate(int length)
    {
        // _vibration.Vibrate(length);
    }

    public void CurrentInitWindow(IWindowCommand currentWindow)
    {
        CurrentWindow = currentWindow;
    }

    public void InitPreviousWindow(IBackToPreviousWindowCommand backToPreviousWindowCommand)
    {
        _backToPreviousWindowCommand.Add(backToPreviousWindowCommand);
    }

    public void StartGame()
    {
        _timerPlayer.Start();
        IsPlay = true;
        _uiData.ButtonPlay.SetNormColor();
    }

    public void StopGame()
    {
        _timerPlayer.Stop();
        IsPlay = false;
        IsPause = false;
        _uiData.ButtonPlay.SetNormColor();
    }


    public void ResetTimeView() => _uiData.WindowTimer.ResetValue();

    public void ActivatePause(bool isPause)
    {
        if (_timerPlayer != null)
            _timerPlayer.ToFreezeTime(isPause);
        IsPause = isPause;
    }


    public void GoOnBackNavigation()
    {
        foreach (var ui in _ui)
        {
            if (ui == CurrentWindow)
            {
                ui.OpenCanvasByPressingEscape(ui);
                CurrentWindow = ui.BackWindowCommand ?? CurrentWindow;
                break;
            }
        }
    }

    public void GoOnBackWindow()
    {
        _ui.ForEach(ui => ui.Hide());
    }

    public void DisableAllUI()
    {
        _ui.ForEach(ui => ui.Hide());
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
             Apllication.Quit();
#elif UNITY_WEBGL
             Application.OpenURL("about:blank");
#endif
    }

    private void OnApplicationQuit()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            YandexGame.SaveProgress();
            //Application.ExternalCall("alert('good buy')");
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
                GoOnBackNavigation();
        }

        if (Input.GetKeyDown("w"))
        {
          GoOnBackNavigation();
        }
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
            Application.ExternalEval("window.focus();");
#endif

        

    }
    
    */
}