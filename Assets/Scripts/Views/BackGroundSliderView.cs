using UnityEngine;

public class BackGroundSliderView : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rectTransformTitle;

    private float deltaScale = (Screen.width > Screen.height) ? ((float)Screen.width / (float)Screen.height) : (1);
    private void Awake()
    {
    //   InitAnchors(_rectTransform);
        //_rectTransformTitle.transform.localScale = Vector2.one * deltaScale;
            

        if (Screen.height <= Screen.width)
        {
      //   _rectTransform.sizeDelta = new Vector2(-(Screen.width-Screen.height), 0);
        }
    }


    private void InitAnchors( RectTransform rectTransform )
    {
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.one * 0.5f;
    }
    
}
