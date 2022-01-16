using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
   public GameObject firstButtonSelect;
   
   public void InitializeButton()
   {
      FindObjectOfType<EventSystem>().SetSelectedGameObject(firstButtonSelect);
   }

   public void Play()
   {
      SceneManager.LoadScene("BAV_HUB_BED");
   }

   public void Exit()
   {
      Application.Quit();
   }
}
