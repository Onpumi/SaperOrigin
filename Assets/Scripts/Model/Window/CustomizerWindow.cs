using UnityEngine;

public class CustomizerWindow
{

    public void InitSizeWindow( Transform transform, float scale = 0.5f )
    {
        if (Screen.width > Screen.height)
        {
            var recTransform = transform.GetComponent<RectTransform>();
            var rectWindow = recTransform.rect;
            var offset = (rectWindow.width - rectWindow.height) * scale;
            recTransform.offsetMin = new Vector2(offset, 0f);
            recTransform.offsetMax = new Vector2(-offset, 0f);
        }
    }
    
}
