using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager instance;
   public int enemyKilled;
   public int moneyObtained;
   public float timer;
   public bool timerStart;
   public int score;

   private int enemyMultiplier;
   private int moneyMultiplier;

   private void Awake()
   {
      if (instance != null && instance != this)
         Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

      instance = this;
   }

   private void Update()
   {
      if (LoadManager.LoadManagerInstance.currentRoom >= 1 && !UIManager.instance.isPaused)
      {
         timerStart = true;
      }
      
      if (UIManager.instance.isPaused)
      {
         timerStart = false;
         UpdateScore();
      }
      
      if (timerStart)
      {
         Timer();
      }

      DisplayTime();
   }

   public void AddEnemyKilledScore(int kill)
   {
      enemyKilled += kill;
   }

   public void AddMoneyObtained(int moneyAdd)
   {
      moneyObtained += moneyAdd;
   }

   public void Timer()
   {
      timer += Time.deltaTime;
   }

   public void DisplayTime()
   {
      float minutes = Mathf.FloorToInt(timer / 60);
      float seconds = Mathf.FloorToInt(timer % 60);
      UIManager.instance.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
   }

   void UpdateScore()
   {
      //based on every data : enemykilled, money obtained;
      score += enemyKilled * enemyMultiplier;
      score += moneyObtained * moneyMultiplier;

      if (timer > 30)
      {
         score += 500;
      }

      if (timer < 30 && timer > 60)
      {
         score += 400;
      }
   }
}
