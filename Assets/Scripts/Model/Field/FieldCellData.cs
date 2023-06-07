using UnityEngine;
   public class FieldCellData
   { 
    public int CountColumns { get; private set; }
    public int CountRows { get; private set; }
    public Vector2 Scale { get; private set; } 

    public FieldCellData(int countColumns, int countRows, Vector2 scale)
    {
        CountColumns = countColumns;
        CountRows = countRows;
        Scale = scale;
    }

    private Vector2 SetLimitValue(Vector2 value, float maxValue)
    {
        var clampScale = value;
        clampScale.x = Mathf.Clamp(value.x, 0, maxValue);
        clampScale.y = Mathf.Clamp(value.y, 0, maxValue);
        return clampScale;
    }

    public void UpdateScale(Vector2 scale)
    {
        Scale = SetLimitValue(Scale, 3f);
    }

    public void AddScaleByVector2(Vector2 value)
    {
        value = SetLimitValue(value, 1f);
        Scale += value;
    }
    
   }
