using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScaleParent : WindowBase
{

    private void Awake() => Hide();
    public override void OpenWindow()
    {
        base.EnableFull();
        Debug.Log("open window");
    }
}
