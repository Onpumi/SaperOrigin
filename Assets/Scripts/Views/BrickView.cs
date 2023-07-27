using TMPro;
using UnityEngine;

public class BrickView : MonoBehaviour, IBrickView
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TMP_Text _text;
    public RectTransform RectTransform => _rectTransform;
    
    private void OnEnable() => transform.gameObject.SetActive(true);
    private void OnDisable() => transform.gameObject.SetActive(false);
    public void SetActive( bool value ) => transform.gameObject.SetActive(value);

    public void SetValue( int i, int j)
    {
        //_text.text = i + "," + j;
        _text.text = "";
    }
    
}
