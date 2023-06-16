using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public  class Views : SerializedMonoBehaviour, IViews
{
    [SerializeField] private MineView _prefabMineView;
    [SerializeField] private CellView _prefabCellView;
    [SerializeField] private FlagView _prefabFlagView;
    [SerializeField] private BrickView _prefabBrickView;
    [SerializeField] private GameField _gameField;
    [SerializeField] private List<IView> _views;
    public MineView MineView => _prefabMineView;
    public CellView CellView => _prefabCellView;
    public FlagView FlagView => _prefabFlagView;
    public BrickView BrickView => _prefabBrickView;
    public GameField GameField => _gameField;
    public List<IView> View => _views;

}
