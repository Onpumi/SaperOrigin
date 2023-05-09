using UnityEngine;
using UnityEngine.EventSystems;

   public class UIInputCheckGame : UIInputCheck
{
    [SerializeField] private IWindowCommand _windowCommand;
    [SerializeField] private TypesOption _typeOption;


    
    private void Start()
    {
        float value = GameField.DataSetting.GameData.GetOptionValue(_typeOption); 
        IsCheckOn = (value == 0f) ? (false) : (true);
        Display();
    }

    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if( _typeOption == TypesOption.SizeCells )
            _windowCommand.OpenWindow();
        else if ( _typeOption == TypesOption.Vibration ||
                  _typeOption == TypesOption.GenerationMinesAfterFirstStep 
                )
        {
            base.OnPointerUp(eventData);
            GameField.DataSetting.GameData.SetupOptionValue(_typeOption, IsCheckOn );
        }
    }
    
}
