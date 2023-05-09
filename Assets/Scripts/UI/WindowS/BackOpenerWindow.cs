
using UnityEngine;

public class BackOpenerWindow : IBackPreviousWindow
{
    public void Open( IWindowCommand window)
    {
        if (window.BackWindowCommand != null)
        {
            window.BackWindowCommand.Enable();
            window.Hide();
        }
    }
    public void Open( IWindowCommand windowCommandFrom, IWindowCommand windowCommandBack )
    {
        if (windowCommandFrom != null && windowCommandBack != null)
        {
            windowCommandBack.Enable();
            windowCommandFrom.Hide();
        }
    }
}
