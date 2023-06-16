using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class WindowScalingBlocks : WindowBase
{
    [SerializeField] private GameState _gameState;
    public float ScaleBricks { get; private set; }
    private GridLayoutGroup _gridLayout;
    private float _resolutionRatio;
    public override void OpenMenuSizeCells() => Open(true);

    private void OnEnable()
    {
        Open(true);
    }

    private void Open(bool value)
    {
        if (_gameState.Views.GameField == null) return;
        ScaleBricks = _gameState.Views.GameField.GameState.GameFieldData.ScaleBrick;
        transform.gameObject.SetActive(value);
        _gridLayout = GetComponent<GridLayoutGroup>();


        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }

        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(value);
        }

        Display();
    }

    public void SetScale(float value)
    {
        ScaleBricks = value;
    }

    public void Display()
    {
        var gameField = _gameState.Views.GameField;
        //if (gameField.ScreenAdjusment != null)
            _gridLayout.cellSize = Vector2.one * 100f * ScaleBricks;
    }
}