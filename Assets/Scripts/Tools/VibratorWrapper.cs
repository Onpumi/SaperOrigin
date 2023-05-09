using UnityEngine;
public  class VibratorWrapper
{
    private AndroidJavaObject vibrator = null;
    
    public VibratorWrapper()
    {
    #if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
        var unityPlayerClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer");
        var unityPlayerActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity" );
        var vibrator = unityPlayerActivity.Call<AndroidJavaObject>( "getSystemService" , "vibrator");
     #endif
    }
    
    public void Vibrate(long time)
    {
      #if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            vibrator.Call( "vibrate", time);
      #endif
    }
    
    
}
