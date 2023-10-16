
public class WindowStatistics : WindowBase
{
    private CustomizerWindow _customizerWindow;

    private void Awake()
    {
        _customizerWindow = new CustomizerWindow();
        Hide();
    }

    private void Start()
    {
        _customizerWindow.InitSizeWindow(transform.GetChild(0));
    }
  
}