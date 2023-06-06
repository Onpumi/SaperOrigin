using UnityEngine;
using UnityEngine.UI;

public class Cell : ICell
{
    private readonly CellView _cellView;
    public Flag Flag { get; private set; }
    public int Value { get; private set; }
    public bool IsOpen { get; private set; }
    public bool IsFlagged { get; private set; }
    public bool IsInitMine { get; private set; }
    public CellData CellData { get; private set; }
    public CellView CellView => _cellView;

    public Cell(CellView cellView)
    {
        Value = 0;
        _cellView = cellView;
        IsOpen = false;
        IsInitMine = false;
        IsFlagged = false;
        CellData = cellView.CellData;
        Flag = new Flag(_cellView);
    }

    public void Display(int indexI, int indexJ, Vector2 scales)
    {
        _cellView.transform.localScale = new Vector3(scales.x, scales.y);
        Image image = _cellView.GetComponent<Image>();
        var width = image.rectTransform.rect.width;
        var height = image.rectTransform.rect.height;
        image.rectTransform.pivot = Vector2.zero;
        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.zero;
        image.rectTransform.anchoredPosition = new Vector2(width * scales.x * indexI, height * scales.y * indexJ);
    }

    public void CreateMine(int value)
    {
        Value = value;
        IsInitMine = (Value == -1);
    }

    public void Open()
    {
        IsOpen = true;
        if (CellView.BrickView != null)
            CellView.BrickView.SetActive(false);
    }

    public bool SetFlag(ContainerMines containerMines)
    {
        if (IsOpen == true) return true;
        Flag.SetFlag(containerMines);
        IsFlagged = Flag.Value;
        return IsFlagged && IsInitMine;
    }

    public void IncrementValue()
    {
        Value++;
        _cellView.SetTextNumbers(Value);
    }
}