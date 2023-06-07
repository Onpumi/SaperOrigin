using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

        private void Awake()
        {
                var rectTransform = GetComponent<RectTransform>();
                
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
        }
    
   
}
