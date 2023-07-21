using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;


public static class IntExtensions
{
    public static int TryThrowIfLessThanZero(this int number)
    {
        if (number < 0)
            throw new ArgumentException("Number can't be less than zero");

        return number;
    }
}

public static class ScreenExtensions
{
    public static void SetPortraitOrientation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}


public static class CanvasExtensions
{
    public static CanvasScaler SetResolutionToScreen(this CanvasScaler canvasScaler, Transform parent = null)
    {
        if (canvasScaler == null)
        {
            if (parent != null) canvasScaler = parent.GetComponent<CanvasScaler>();
        }

        canvasScaler.referenceResolution = new UnityEngine.Vector2(Screen.width, Screen.height);
        return canvasScaler;
    }
}