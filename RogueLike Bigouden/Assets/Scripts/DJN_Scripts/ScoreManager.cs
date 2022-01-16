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

   [HideInInspector] public float seconds;
   [HideInInspector] public float minutes;

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
      UpdateScore();
   }

   public void AddMoneyObtained(int moneyAdd)
   {
      moneyObtained += moneyAdd;
      UpdateScore();
   }

   public void Timer()
   {
      timer += Time.deltaTime;
   }

   public void DisplayTime()
   {
       minutes = Mathf.FloorToInt(timer / 60);
       seconds = Mathf.FloorToInt(timer % 60);
      UIManager.instance.timeText.text = $"{minutes:00}:{seconds:00}";
   }

   public void UpdateScore()
   {
      //based on every data : enemykilled, money obtained;
      if (timer > 180)
      {
         score = (enemyKilled * enemyMultiplier) + (moneyObtained * moneyMultiplier) + 500;
      }

      if (timer > 180 && timer < 360)
      {
         score = (enemyKilled * enemyMultiplier) + (moneyObtained * moneyMultiplier) + 300;
      }

      if (timer > 360)
      {
         score = (enemyKilled * enemyMultiplier) + (moneyObtained * moneyMultiplier);
      }
   }
}
