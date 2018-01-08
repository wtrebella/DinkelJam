using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameReport : MonoBehaviour 
{
   [SerializeField] private Text _finalGradeText;
   [SerializeField] private Text _finalPercentText;
   [SerializeField] private SculputreStatItem _sculptureStatItemTemplate;
   
   private List<SculputreStatItem> _statItems = new List<SculputreStatItem>();
   
   public void AddStatItem(string name, float percent)
   {
      SculputreStatItem statItem = Instantiate(_sculptureStatItemTemplate, _sculptureStatItemTemplate.transform.parent);
      statItem.gameObject.SetActive(true);
      string grade = GetGrade(percent);
      statItem.Init(name, percent, grade);
      _statItems.Add(statItem);
   }
   
   public void CalculateFinalGrade()
   {
      float total = 0;
      
      for (int i = 0; i < _statItems.Count; i++)
      {
         SculputreStatItem statItem = _statItems[i];
         total += statItem.GetPercent();
      }
      
      float average = total / _statItems.Count;
      _finalPercentText.text = average.ToString("0.0") + "%";
      _finalGradeText.text = GetGrade(average);
   }
   
   string GetGrade(float percent)
   {
      if (percent < 60.0f) return "F";
      else if (percent < 62.0f) return "D-";
      else if (percent < 68.0f) return "D";
      else if (percent < 70.0f) return "D+";
      else if (percent < 72.0f) return "C-";
      else if (percent < 78.0f) return "C";
      else if (percent < 80.0f) return "C+";
      else if (percent < 82.0f) return "B-";
      else if (percent < 88.0f) return "B";
      else if (percent < 90.0f) return "B+";
      else if (percent < 92.0f) return "A-";
      else if (percent < 98.0f) return "A";
      else return "A+";
   }
}
