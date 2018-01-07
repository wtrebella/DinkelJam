using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultisceneLoader : Singleton<MultisceneLoader> 
{
   [SerializeField] private Multiscene _mainMenuMultiscene;
   [SerializeField] private Multiscene _gameMultiscene;
   
   public void LoadMultiscene(Multiscene multiscene)
   {
      multiscene.LoadScenes();
   }
   
   public void LoadMainMenu()
   {
      LoadMultiscene(_mainMenuMultiscene);
   }
   
   public void LoadGame()
   {
      LoadMultiscene(_gameMultiscene);
   }
}
