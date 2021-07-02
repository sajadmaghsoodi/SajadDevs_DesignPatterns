using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   public static event Action OnScore;
   
   public static ScoreManager Instance;

   [SerializeField] private TextMeshProUGUI _scoreText;
   
   private int _score;

   private void Awake()
   {
      Instance = this;
      UpdateUi();
   }

   public void AddScore(int addedScore)
   {
      _score += addedScore;
      UpdateUi();
   }

   public void UpdateUi()
   {
      OnScore?.Invoke();
      _scoreText.text = _score.ToString();
   }
}
