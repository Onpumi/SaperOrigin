using UnityEngine;
using UnityEngine.UI;


public class SliderProgress : MonoBehaviour
{
     private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.transform.gameObject.SetActive(false);
    }
    public void UpdateValue( float value )
    {
        _slider.value = value;
    }
}
