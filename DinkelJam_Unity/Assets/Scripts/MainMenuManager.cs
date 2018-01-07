using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour 
{
   private float _startTime = 0;

   void Awake()
   {
      _startTime = Time.time;  
   }

   void Update()
   {
      float elapsedTime = Time.time - _startTime;

      if (elapsedTime > 1.0f)
      {
         if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
         {
            MultisceneLoader.instance.LoadGame();
         }
      }
   }
}