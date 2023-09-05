using UnityEngine;
using UnityEngine.UI;


public class SliderProgress : MonoBehaviour
{
     private RectTransform _rectTransform;
     private Slider _slider;
     
    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _rectTransform = _slider.transform.GetComponent<RectTransform>();

//        _rectTransform.anchorMin = new Vector2(0.2f,0.48f);
//        _rectTransform.anchorMax = new Vector2(0.8f,0.52f);
        
        _rectTransform.pivot = Vector2.zero;
        _rectTransform.anchoredPosition = Vector2.one * 0.5f;
        transform.gameObject.SetActive(false);
    }
    public void UpdateValue( float value )
    {
        _slider.value = value;
    }


    public void SetSizes()
    {
        _rectTransform.anchorMin = new Vector2(0.5f,0.48f);
        _rectTransform.anchorMax = new Vector2(0.5f,0.52f);
        _rectTransform.anchoredPosition = Vector2.one * 0.5f;
        _rectTransform.pivot = Vector2.zero;
        var deltaOffset = -10f;
        var offset = Mathf.Min(Screen.height,Screen.width) * 0.5f + deltaOffset;
        _rectTransform.offsetMin = new Vector2(-offset, 0);
        _rectTransform.offsetMax = new Vector2(offset, 0);;
    }
    
}
