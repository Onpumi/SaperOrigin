using UnityEngine;

public class BorderField : MonoBehaviour
{
    [SerializeField] private RectTransform _prefabImage;
    [SerializeField] private RectTransform _rectTransformCanvas;
    [SerializeField] private RectTransform _prefabBottomLeftCorner;
    [SerializeField] private RectTransform _prefabTopLeftCorner;
    [SerializeField] private RectTransform _prefabBottomRightCorner;
    [SerializeField] private RectTransform _prefabTopRightCorner;
    [SerializeField] private RectTransform _prefabUpDownBorder;
    [SerializeField] private RectTransform _prefabLeftBorder;
    [SerializeField] private RectTransform _prefabRightBorder;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _sizeBorder = 0.5f;
    public RectTransform RectTransform => _rectTransform;
    
    private RectTransform _border;
    private Vector2 _offsetWorld;
    public float WidthImage => _prefabImage.rect.width;
    public float HeightImage => _prefabImage.rect.height;

    public void Init(RectTransform rectTransformField)
    {
        var corners = new Vector3[4];
        rectTransformField.GetWorldCorners(corners);
        var bottomLeftCorner = corners[0];
        var topLeftCorner = corners[1];
        var topRightCorner = corners[2];
        var bottomRightCorner = corners[3];
        var lossyScale = _rectTransformCanvas.lossyScale;
        _offsetWorld = new Vector2(WidthImage * lossyScale.x, HeightImage * lossyScale.y);
        DrawBorder(new Vector3[4] { bottomLeftCorner, topLeftCorner, topRightCorner, bottomRightCorner });
    }

    private void SpawnImage(RectTransform image)
    {
        _border = Instantiate(image, transform.parent);
    }

    private void DrawBorder(Vector3[] corners)
    {
        /*
        DrawLine(corners[1], corners[2], -Vector2.left, Vector2.up, _prefabUpDownBorder);
        DrawLine(corners[0], corners[3], -Vector2.left, -Vector2.up, _prefabUpDownBorder);
        DrawLine(corners[0], corners[1], Vector2.up, Vector2.left, _prefabLeftBorder);
        DrawLine(corners[3], corners[2], Vector2.up, -Vector2.left, _prefabRightBorder);
        DrawImage(corners[0], new Vector2(-1f, -1f), _prefabBottomLeftCorner);
        DrawImage(corners[1], new Vector2(-1f, 1f), _prefabTopLeftCorner);
        DrawImage(corners[2], new Vector2(1f, 1f), _prefabTopRightCorner);
        DrawImage(corners[3], new Vector2(1f, -1f), _prefabBottomRightCorner);
        */
    }

    private void DrawImage(Vector3 position, Vector2 offset, RectTransform image)
    {
        SpawnImage(image);
        var scale = _border.localScale ;
        _border.transform.position = position;
        _border.anchoredPosition += offset * scale * _border.rect.width / 2f;
    }

    private void DrawLine(Vector3 firstPosition, Vector3 lastPosition, Vector2 offsetDrawDirection, Vector2 offsetFirst,
        RectTransform border)
    {
        var scale = border.localScale;
        var sizeBorder = border.rect.width;
        DrawImage(firstPosition, offsetFirst, border);
        var distanceCorner = (lastPosition - firstPosition).magnitude;
        var countDrawImage = distanceCorner / _offsetWorld.x * 2f;

        for (int i = 0; i < countDrawImage; i++)
        {
            DrawImage(_border.position, new Vector2(0f, 0f), border);
            _border.anchoredPosition += offsetDrawDirection * scale * sizeBorder / 2f;
        }
    }
    
  
}