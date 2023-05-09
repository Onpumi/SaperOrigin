using UnityEngine;

public class UIHeader : MonoBehaviour
{
    private void Awake()
    {
        var safeArea = Screen.safeArea;
        var rectTransform = GetComponent<RectTransform>();
        var delta = Screen.height-safeArea.height;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, delta);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0,   rectTransform.sizeDelta.y);
    }
}
