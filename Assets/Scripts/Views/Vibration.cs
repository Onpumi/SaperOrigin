using UnityEngine;

public class Vibration : WindowBase
{
 //   [SerializeField] private ClassForJavaScript _classForJavaScript;

    public void Awake()
    {
        Hide();
    }
    
    public void Vibrate(int length)
    {
       // _classForJavaScript.Vibrate( length );
    //    Application.ExternalEval("VibrateJS();");
    }

    
    
}
