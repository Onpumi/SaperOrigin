using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class WindowBase : SerializedMonoBehaviour, IWindowCommand
{

    public virtual IWindowCommand BackWindowCommand { get; }
    public bool IsActive => transform.gameObject.activeSelf;
    public virtual void OpenMenuSizeCells() { }
    public virtual void Hide() => gameObject.SetActive(false);
    protected void Open() => gameObject.SetActive(true);
    public virtual void OpenCanvasByPressingEscape( IWindowCommand windowCommand ) { }

    public virtual void ConfirmAction( bool value ) { }
    
    public virtual void Display(List<IWindowCommand> activeUI) { }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
        transform.SetAsFirstSibling();
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

    }

    public void EnableFull()
    {
        Enable();
        ActivateAllChild(transform);
    }

    private void ActivateAllChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            if (child.childCount > 0)
            {
                ActivateAllChild(child);
            }
        }
    }
    public virtual void OpenWindow()
    {
    }

}
