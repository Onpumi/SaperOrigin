using UnityEngine;
using UnityEngine.UI;

public class SliderSizeCells : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private WindowScalingBlocks _windowScalingBlocks;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(delegate { ChangeScaleBrick(); });
    }

    public override void OpenMenuSizeCells()
    {
        var gameField = _gameState.Views.GameField;
        _slider.value = _windowScalingBlocks.ScaleBricks;
        if (Screen.width < Screen.height)
            _slider.maxValue = gameField.CalculateScale() * 2f;
        else
            _slider.maxValue = gameField.CalculateScale() * 1.5f;
        _slider.minValue = gameField.CalculateScale() * 0.5f;
        Open();
    }

    private void ChangeScaleBrick()
    {
        _windowScalingBlocks.SetScale(_slider.value);
        _windowScalingBlocks.Display();
    }
}