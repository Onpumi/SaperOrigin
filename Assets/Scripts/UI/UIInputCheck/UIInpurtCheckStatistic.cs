using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class UIInpurtCheckStatistic : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private DifficultType _currentTypeGame;
    private Button _button;
    private Image _image;
    private Color _activeColor;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _activeColor = _image.color;
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeDifficult);
        ChangeDifficult();
    }

    public void ChangeDifficult()
    {
        //Debug.Log(_gameState.UIData.GameField.DifficultLevel);
        _gameState.UIData.GameField.SetPercentMine((TypesGame)_currentTypeGame.Type);
        _gameState.UIData.GameField.DataSetting.GameData.SetupOptionValue(_gameState.UIData.GameField.DifficultLevel);


        if (_currentTypeGame.Type != (int)_gameState.UIData.GameField.DifficultLevel)
        {
            Color color = _image.color;
            color.a = 0.5f;
            _image.color = color;
        }
        else
        {
            _image.color = _activeColor;
        }
    }
}