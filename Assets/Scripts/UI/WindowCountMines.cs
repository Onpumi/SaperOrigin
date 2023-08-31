using UnityEngine;
using UnityEngine.UI;

public class WindowCountMines : WindowBase
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private MenuBarView _topMenu;
    [SerializeField] private FlagView _flagViewPrefab;
    [SerializeField] private Transform _parentFlagIcon;
    private DigitalView[] _digitalViews;
    private GridLayoutGroup _gridLayoutGroup;
    private DigitalBuilder _digitalBuilder;
    private FlagView _flagIcon;

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
        transform.localScale = Vector3.one * 0.8f;
    }

    public void ResetValue(int count)
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
        DisplayFlagIcon();
    }

    private void DisplayFlagIcon()
    {
        if (_flagIcon == null)
        {
            _flagIcon = Instantiate(_flagViewPrefab, _parentFlagIcon) as FlagView;
            var rectTransform = GetComponent<RectTransform>();
            var rectTransformFlag = _flagIcon.GetComponent<RectTransform>();
            rectTransformFlag.anchoredPosition = Vector2.zero + Vector2.left * rectTransform.rect.width;
            _flagIcon.Display();
        }
        else
            _flagIcon.Display();
    }
}