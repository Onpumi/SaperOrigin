using UnityEngine;

public class ScaleCalculator
{
    private Vector2 _scalingFactor;
    private Rect _rectParent;
    private Rect _rectImage;
    private FieldCellData _fieldCellData;
    private BackgroundField _backgroundField;

    public ScaleCalculator(BackgroundField backgroundField, Rect rectImage, FieldCellData fieldCellData)
    {
        _rectParent = backgroundField.Rect;
        _rectImage = rectImage;
        _fieldCellData = fieldCellData;
        _backgroundField = backgroundField;
    }

    public Vector2 GetFixedScales()
    {
        var parentWidth = _rectParent.width;
        var parentHeight = _rectParent.height;
        var imageWidth = _rectImage.width;
        var imageHeight = _rectImage.height;
        _scalingFactor.x =
            parentWidth * _backgroundField.transform.localScale.x / (imageWidth * _fieldCellData.CountColumns) -
            _fieldCellData.Scale.x;
        _scalingFactor.y =
            parentHeight * _backgroundField.transform.localScale.y / (imageHeight * _fieldCellData.CountRows) -
            _fieldCellData.Scale.y;
        _fieldCellData.AddScaleByVector2(_scalingFactor);
        return _fieldCellData.Scale;
    }
}