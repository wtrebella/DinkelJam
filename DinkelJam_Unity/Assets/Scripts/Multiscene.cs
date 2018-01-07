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
   [SerializeField] private string _baseSceneName;
   [SerializeField] private string[] _additiveSceneNames;
   [SerializeField] private string _singleScenesPath = "/Scenes/SingleScenes/";
         
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
      
      EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
      EditorSceneManager.OpenScene(GetScenePath(_baseSceneName), OpenSceneMode.Single);

      for (int i = 0; i < _additiveSceneNames.Length; i++)
      {
         string additiveSceneName = _additiveSceneNames[i];
         EditorSceneManager.OpenScene(GetScenePath(additiveSceneName), OpenSceneMode.Additive);
      }
   }
}
