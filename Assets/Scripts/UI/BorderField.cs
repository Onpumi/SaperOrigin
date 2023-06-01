using UnityEngine;

public class BorderField : MonoBehaviour
{
    [SerializeField] private RectTransform _prefabImage;
    private RectTransform _border;

    public void Init( RectTransform rectTransformField )
    {
        Vector3[] corners = new Vector3[4];
        rectTransformField.GetWorldCorners(corners);
        Vector3 bottomLeftCorner = corners[0];
        Vector3 topLeftCorner = corners[1];
        Vector3 topRightCorner = corners[2];
        Vector3 bottomRightCorner = corners[3];

        DrawImage( bottomLeftCorner, new Vector2(-1f,-1f) );
        DrawImage( topLeftCorner, new Vector2(-1f, 1f) );
        DrawImage( topRightCorner, new Vector2(1f, 1f) );
        DrawImage( bottomRightCorner, new Vector2(1f, -1f) );
        DrawLine(topLeftCorner,topRightCorner, -Vector2.left, Vector2.up);
        DrawLine(bottomLeftCorner,bottomRightCorner, -Vector2.left, -Vector2.up);
        DrawLine(bottomLeftCorner,topLeftCorner, Vector2.up, Vector2.left);
        DrawLine(bottomRightCorner,topRightCorner, Vector2.up, -Vector2.left);
    }

    private void SpawnImage() => _border = Instantiate(_prefabImage, transform.parent);

    private void DrawImage( Vector3 position, Vector2 offset  )
    { 
        SpawnImage();
        var scale = _border.localScale;
       _border.transform.position = position;
       _border.anchoredPosition += offset * scale * _border.rect.width / 2f;
    }

    private void DrawLine( Vector3 firstPosition, Vector3 lastPosition, Vector2 offsetDrawDirection, Vector2 offsetFirst )
    {
        var scale = _border.localScale;
        var sizeBorder = _border.rect.width;
       DrawImage( firstPosition, offsetFirst );
        while( Vector2.SqrMagnitude(lastPosition-_border.position) > 0.001f )
        {
            DrawImage(_border.position, new Vector2(0f, 0f));
            _border.anchoredPosition += offsetDrawDirection * scale * sizeBorder / 2f;
        }
    }
    
}
