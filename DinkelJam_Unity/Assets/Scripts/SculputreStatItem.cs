using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SculputreStatItem : MonoBehaviour {
   [SerializeField] private Text _nameText;
   [SerializeField] private Text _percentText;
   [SerializeField] private Text _gradeText;
   
   private float _percent;
   
   public void Init(string name, float percent, string grade)
   {
      _percent = percent;
      _nameText.text = name;
      _percentText.text = percent.ToString("0.0") + "%";
      _gradeText.text = grade;
   }
   
   public float GetPercent()
   {
      return _percent;
   }
}
