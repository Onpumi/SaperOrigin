using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCalculator
{
    private Vector2 _scalingFactor;
    private Rect _rectParent;
    private Rect _rectImage;
    private FieldCellData _fieldCellData;

    public ScaleCalculator( Rect rectParent, Rect rectImage, FieldCellData fieldCellData )
    {
        _rectParent = rectParent;
        _rectImage = rectImage;
        _fieldCellData = fieldCellData;
    }

    public Vector2 GetFixedScales()
    {
        var parentWidth = _rectParent.width;
        var parentHeight = _rectParent.height;
        var imageWidth = _rectImage.width;
        var imageHeight = _rectImage.height;
        _scalingFactor.x = parentWidth / (imageWidth * _fieldCellData.CountColumns) - _fieldCellData.Scales.x;
        _scalingFactor.y = parentHeight / (imageHeight * _fieldCellData.CountRows) - _fieldCellData.Scales.y;
        _fieldCellData.Scales.x += _scalingFactor.x;
        _fieldCellData.Scales.y += _scalingFactor.y;
        return _fieldCellData.Scales;
    }

}
