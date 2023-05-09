 public class TimerPlayer 
{
    private readonly WindowTimer _windowTimer;

    public TimerPlayer(WindowTimer windowTimer)
    {
        _windowTimer = windowTimer;
    }

    public void Start()
    {
        _windowTimer.StartTimer();
    }

    public void Stop()
    {
        _windowTimer.StopTimer();
    }

    public void ToFreezeTime( bool isPause)
    {
        _windowTimer.PauseTime(isPause);
    }

}