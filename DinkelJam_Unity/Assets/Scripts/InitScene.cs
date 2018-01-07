using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour 
{
   [SerializeField] private Multiscene _multisceneToLoad;
   
   void Start()
   {
      _multisceneToLoad.LoadScenes();  
   }
}
