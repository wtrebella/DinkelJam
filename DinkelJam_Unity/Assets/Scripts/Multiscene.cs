using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;

[CreateAssetMenu(fileName = "New Multiscene")]
public class Multiscene : ScriptableObject 
{
   private const string _singleScenesPath = "/Scenes/Single Scenes/";
   
   [SerializeField] private string _baseSceneName;
   [SerializeField] private string[] _additiveSceneNames;
         
   public string GetBaseSceneName()
   {
      return _baseSceneName;
   }
   
   public string[] GetAdditiveSceneNames()
   {
      return _additiveSceneNames;  
   }
   
   [OnOpenAsset()]
   static bool OnOpenAsset(int instanceID, int line) 
   {
      if (!(EditorUtility.InstanceIDToObject(instanceID) is Multiscene)) return false;
      
      Multiscene multiscene = (Multiscene)EditorUtility.InstanceIDToObject(instanceID);
      multiscene.LoadScenes();
      return true;
   }
   
   string GetScenePath(string sceneName)
   {
      return Application.dataPath + _singleScenesPath + sceneName + ".unity";
   }
   
   public void LoadScenes()
   {
      if (string.IsNullOrEmpty(_baseSceneName)) return;
      
      if (Application.isPlaying)
      {
         SceneManager.LoadScene(_baseSceneName, LoadSceneMode.Single);
         
         for (int i = 0; i < _additiveSceneNames.Length; i++)
         {
            string additiveSceneName = _additiveSceneNames[i];
            SceneManager.LoadScene(additiveSceneName, LoadSceneMode.Additive);
         }
      }
      else
      {
         string baseScenePath = GetScenePath(_baseSceneName);
         
         EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
         EditorSceneManager.OpenScene(baseScenePath, OpenSceneMode.Single);
   
         for (int i = 0; i < _additiveSceneNames.Length; i++)
         {
            string additiveSceneName = _additiveSceneNames[i];
            string additiveScenePath = GetScenePath(additiveSceneName);
            EditorSceneManager.OpenScene(additiveScenePath, OpenSceneMode.Additive);
         }
      }
   }
}
