using UnityEngine;
using UnityEngine.UI;

public class SliderSizeCells : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private WindowScalingBlocks _windowScalingBlocks;

    private readonly float _minScaleValue = 1f;
    private readonly float _maxScaleValue = (Screen.width < Screen.height) ? (2.5f) : (2f);
    
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
        _slider.maxValue = gameField.GetScale() * _maxScaleValue;
        _slider.minValue = gameField.GetScale() * _minScaleValue;
        Open();
    }

    private void ChangeScaleBrick()
    {
        _windowScalingBlocks.SetScale(_slider.value);
        _windowScalingBlocks.Display();
    }
}