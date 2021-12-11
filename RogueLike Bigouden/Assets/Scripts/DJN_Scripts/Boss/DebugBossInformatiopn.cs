using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBossInformatiopn : MonoBehaviour
{
   public TextMeshProUGUI healthText;
   public PillarsStatsManager PillarsStatsManager;


   private void Update()
   {
      healthText.text = PillarsStatsManager.health.ToString();
   }
}
