using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public abstract class UIInputCheck : SerializedMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private UICheckButton _uiCheckButton;
    
    protected bool IsCheckOn { get; set; }
    protected GameField GameField => _gameState.Views.GameField;

    public void OnPointerDown(PointerEventData eventData ) { }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        IsCheckOn = !IsCheckOn;
        Display();
    }
    protected void Display()
    {
         
        if (_uiCheckButton != null)
        {
            _uiCheckButton.SetSprite(IsCheckOn);
        }
        
    }
}