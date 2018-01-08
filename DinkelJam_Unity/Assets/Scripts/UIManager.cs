using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
   [SerializeField] private PostGameReport _postGameReport;
   [SerializeField] private GameObject _timer;
   [SerializeField] private Text _timerText;
   
   void Awake()
   {
      GameManager.instance.SignalGameStart += OnGameStart;
      GameManager.instance.SignalGameOver += OnGameOver;  
      
      _postGameReport.gameObject.SetActive(false);
      _timer.gameObject.SetActive(false);
   }
   
   void OnDestroy()
   {
      if (GameManager.DoesExist())
      {
         GameManager.instance.SignalGameStart -= OnGameStart;
         GameManager.instance.SignalGameOver -= OnGameOver;  
      }
   }
   
   void Update()
   {
      if (_timer.gameObject.activeSelf)
      {
         float timerVal = GameManager.instance.GetGameTimer();
         string timerString = timerVal.ToString("0.0");
         _timerText.text = timerString;
      }
   }
   
   void OnGameOver()
   {
      _postGameReport.gameObject.SetActive(true);
      _timer.gameObject.SetActive(false);
      
      _postGameReport.AddStatItem("Curvy Banana", 58.3f);
      _postGameReport.AddStatItem("Block Boy", 91.4f);
      _postGameReport.AddStatItem("Dog Balls", 82.3f);
      _postGameReport.AddStatItem("Poophead", 56.8f);
      
      _postGameReport.CalculateFinalGrade();
   }
   
   void OnGameStart()
   {
      _postGameReport.gameObject.SetActive(false);
      _timer.gameObject.SetActive(true);
   }
}
