using UnityEngine;
using UnityEngine.UI;

public class WindowCountMines : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private MenuBarView _topMenu;
    private DigitalView[] _digitalViews;
    private GridLayoutGroup _gridLayoutGroup;
    private DigitalBuilder _digitalBuilder;

    private void Start()
    {
        gameObject.SetActive(false);
        _digitalViews = new DigitalView[transform.childCount];
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _digitalBuilder = new DigitalBuilder(_sprites, _digitalViews);
        int count = 0;
        foreach (Transform view in transform)
        {
            _digitalViews[count] = view.GetComponent<DigitalView>();
            count++;
        }

        InitSizeFieldTime();
    }

    public void ResetValue( int count )
    {
        _digitalBuilder.Display(count);
    }

    private void InitSizeFieldTime()
    {
        var widthCell = _topMenu.Height / 2f;
        var heightCell = _topMenu.Height;
        _gridLayoutGroup.cellSize = new Vector2(widthCell, heightCell);
    }

    public void Display(int count)
    {
        gameObject.SetActive(true);
        _digitalBuilder.Display(count);
    }
    

    
}