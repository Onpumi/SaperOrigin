
using UnityEngine;

public struct CellData
{
    public int Index1;
    public int Index2;
    public Vector2 Scales;

    public CellData(int index1, int index2, Vector2 scales)
    {
        Index1 = index1;
        Index2 = index2;
        Scales = scales;
    }
}
