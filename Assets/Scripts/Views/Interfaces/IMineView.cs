using UnityEngine; 
public interface IMineView : IView
{
    public void ActivateMine( bool isExposion );
    public void SetActive(bool value);
}
