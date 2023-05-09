using UnityEngine;


public interface IFlagView : IView
{
    public bool Value { get; }
    public void InitFlag(bool value);
    public void SetFlagError();
    public void SetActive(bool value);
    public void Display();

}
