using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCutter : MonoBehaviour
{
   [SerializeField] private FracturedObject _fracturedObject;
   
   private List<GameObject> _manChunks;
   
   void Awake()
   {

   }
   
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         UltimateFracturing.Fracturer.FractureToChunks(_fracturedObject, true, out _manChunks, OnProgress);
      }
   }
   
   void OnProgress(string title, string message, float alpha)
   {
      Debug.Log(title + ": " + message + ", " + alpha);
   }
}
