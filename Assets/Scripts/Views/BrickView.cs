using UnityEngine;

public class BrickView : MonoBehaviour, IBrickView
{
    [SerializeField] private RectTransform _rectTransform;
    public RectTransform RectTransform => _rectTransform;
    private void OnEnable() => transform.gameObject.SetActive(true);
    private void OnDisable() => transform.gameObject.SetActive(false);
    public void SetActive( bool value ) => transform.gameObject.SetActive(value);
    
    
    
}
