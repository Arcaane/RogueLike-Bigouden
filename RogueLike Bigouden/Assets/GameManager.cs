using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   void Awake()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }
}
