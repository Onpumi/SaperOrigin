using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class SectionMenuView : MonoBehaviour
{
    [SerializeField] private MenuType MenuType;
    [SerializeField] private RectTransform _centerImage;
    [SerializeField] private RectTransform _parentRectTransform;
    [SerializeField] private BorderField _borderField;
    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private int _countChild;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>() ??
                         throw new ArgumentException("RectTransform in SectionMenu is null");
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

        _countChild = transform.childCount;
        

        if (MenuType != MenuType.Center)
        {
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }

        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        SetProperties();
    }

    private void SetProperties()
    {
        var parentRect = _parentRectTransform.rect;
        var heightParent = parentRect.height;
        
        if (MenuType == MenuType.Left)
        {
            _rectTransform.anchorMin = new Vector2(0f, 0f);
            _rectTransform.anchorMax = new Vector2(0.5f, 1f);
            _rectTransform.offsetMax = new Vector2(-heightParent/2f,0f);
        }
        else if (MenuType == MenuType.Right)
        {
            _rectTransform.anchorMin = new Vector2(0.5f, 0f);
            _rectTransform.anchorMax = new Vector2(1f, 1f);
            
            if (_borderField != null)
            {
                /*
                _rectTransform.offsetMin = new Vector2(heightParent / 2f + _borderField.WidthImage,
                    0f + _borderField.HeightImage);
                _rectTransform.offsetMax = new Vector2(-_borderField.WidthImage, -_borderField.HeightImage);
                */
                _rectTransform.offsetMin = new Vector2(heightParent / 2f,
                    0f);
                //_rectTransform.offsetMax = new Vector2(-_borderField.WidthImage, -_borderField.HeightImage);
            }
            
        }
        else if (MenuType == MenuType.Center)
        {
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.sizeDelta = new Vector2(heightParent, heightParent);
        }
        
        
        if (MenuType != MenuType.Center)
        {
            var rectMenu = _rectTransform.rect;
            var width = rectMenu.width;
            var withParent = _parentRectTransform.rect.width;
            var height = rectMenu.height;
            var halfWidth = rectMenu.width * 0.5f;
            var lengthSpace = 0;
            var countBlocks = _countChild;
            var countSpaces = _countChild + 1;
            var cellSizeX = (width - countSpaces * lengthSpace) / countBlocks;
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _countChild;
            _gridLayoutGroup.padding.left = (int)lengthSpace;
            _gridLayoutGroup.padding.right = (int)lengthSpace;
            _gridLayoutGroup.spacing = new Vector2(lengthSpace, 0);
            _gridLayoutGroup.cellSize = new Vector2(withParent / 4f,height); 
        }
    }

    public float GetWidth()
    {
        return _rectTransform.rect.width;
    }
    
    public void InitCellSize( float size )
    {
        _gridLayoutGroup.cellSize = new Vector2( size,_rectTransform.rect.height);
    }
    
    
}