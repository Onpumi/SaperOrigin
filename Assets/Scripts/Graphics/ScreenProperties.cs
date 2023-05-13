using UnityEngine;
using UnityEngine.UI;
using YG;

public class ScreenAdjusment
{
    private CanvasScaler _canvasScaler;
    private readonly Transform _canvasParent;
    public readonly Vector2 ResolutionCanvas;
    public readonly float PixelsPerUnit;
    public float RefPixelsPerUnit { get; private set; }

    public ScreenAdjusment(Transform canvasParent)
    {
        _canvasParent = canvasParent;
        ScreenExtensions.SetPortraitOrientation();
        _canvasScaler = _canvasParent.GetComponent<CanvasScaler>();
        if( _canvasScaler == null ) return;
  //      _canvasScaler.SetResolutionToScreen();
        ResolutionCanvas = _canvasScaler.referenceResolution;
        RefPixelsPerUnit = _canvasScaler.referencePixelsPerUnit;
        PixelsPerUnit = _canvasScaler.referencePixelsPerUnit;
        Screen.fullScreen = true;
    }
}