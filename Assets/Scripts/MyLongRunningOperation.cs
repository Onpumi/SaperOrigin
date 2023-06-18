using System.Collections;
using UnityEngine;

public class MyLongRunningOperation
{
     private float _progress = 0;

     public float Progress
     {
          get { return _progress;  }
     }

     public IEnumerator Execute()
     {
          for (int i = 0; i < 100; i++)
          {
               _progress = i / 100f;

               yield return null;
          }
     }
}
