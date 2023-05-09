using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public  class Views : SerializedMonoBehaviour, IViews
{
    [SerializeField] private MineView _mineView;
    [SerializeField] private CellView _cellView;
    [SerializeField] private FlagView _flagView;
    [SerializeField] private BrickView _brickView;
    [SerializeField] private BoomView _boomView;
    [SerializeField] private GameField _gameField;
    [SerializeField] private List<IView> _views;
    public MineView MineView => _mineView;
    public CellView CellView => _cellView;
    public FlagView FlagView => _flagView;
    public BrickView BrickView => _brickView;
    public IBoomView BoomView => _boomView;
    public GameField GameField => _gameField;
    public List<IView> View => _views;

}
