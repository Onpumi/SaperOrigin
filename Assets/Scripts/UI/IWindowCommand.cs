


public interface IWindowCommand
{
    public bool IsActive { get;  }
    public IWindowCommand BackWindowCommand { get; }
    public void OpenWindow();
    public void Hide();
    public void OpenCanvasByPressingEscape( IWindowCommand iui );
    public void ConfirmAction( bool value );
    public void Enable();
    public void EnableFull();
}
