using System;
using UnityEngine;
using UnityEngine.UI;

public class ScalerRowMenu : MonoBehaviour
{

    [SerializeField] private WindowSettings _windowSettings;
   private GridLayoutGroup _gridLayout;


   private void Start()
   {
       _gridLayout = GetComponent<GridLayoutGroup>() ??  throw new ArgumentException("GridLayoutGroup is null"); ;
       _gridLayout.childAlignment = TextAnchor.MiddleCenter;
       _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
       _gridLayout.constraintCount = 1;
       _gridLayout.padding.left = 0;
       _gridLayout.padding.right = 0;
       _gridLayout.padding.top = 0;
   }

   
   

}
