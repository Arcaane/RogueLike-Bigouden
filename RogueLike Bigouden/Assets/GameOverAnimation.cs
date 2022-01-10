using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
   public Animator restartButton;
   public Animator quitButton;

   public void LoadButtonGO()
   {
      restartButton.Play("button_gameover");
      quitButton.Play("button_gameover");
      UIManager.instance.SetSelectedButton(restartButton.gameObject);
   }

   public void PlaySound(string soundName)
   {
      SoundManager.instance.PlaySound(soundName);
   }
}
