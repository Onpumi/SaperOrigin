using UnityEngine;
using System.Runtime.InteropServices; 
public class ClassForJavaScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityPluginVibrateJs();
    [DllImport("__Internal")]
    private static extern void UnityPluginTestJs();
    public void Vibrate( int length )
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UnityPluginVibrateJs();
#endif        
    }

    public void Test()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UnityPluginTestJs();
#endif
    }
}
