using UnityEngine;
   public struct FieldCellData
   { 
    public int CountColumns;
    public int CountRows;
    public Vector2 Scales; 

    public FieldCellData(int countColumns, int countRows, Vector2 scales)
    {
        CountColumns = countColumns;
        CountRows = countRows;
        Scales = scales;
    }

    public void UpdateScales(Vector2 scales)
    {
        Scales = scales;
    }
   }
