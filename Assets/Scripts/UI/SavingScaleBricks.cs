using System;
using UnityEngine;
using UnityEngine.UI;

public class SavingScaleBricks : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private WindowScaleBricks _windowScaleBricks;
    [SerializeField] private WindowScalingBlocks _windowScalingBlocks;
    private Button _buttonSave;

    private void Awake()
    {
        _buttonSave = transform.GetChild(0).GetComponent<Button>()
                      ?? throw new ArgumentException("You try to get null saving button");

        _buttonSave.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _gameState.Views.GameField.SaveScaleValueBricks(TypesOption.SizeCells, _windowScalingBlocks);
        _gameState.Views.GameField.DestroyAll();
        _gameState.Views.GameField.ResetField();   
        _gameState.Views.GameField.GeneratePool();
        _gameState.Views.GameField.ReloadField();
        _windowScaleBricks.Hide();
    }
}