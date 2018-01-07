using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Multiscene))]
public class MultisceneEditor : Editor 
{
   Multiscene _multiscene;
   
   void OnEnable()
   {
      _multiscene = (Multiscene)target;  
   }
   
   public override void OnInspectorGUI()
   {
      DrawDefaultInspector();
            
      string baseSceneName = _multiscene.GetBaseSceneName();
      
      if (baseSceneName != null)
      {
         if (GUILayout.Button("Load Scenes"))
         {
            _multiscene.LoadScenes();
         }
      }
   }
}
