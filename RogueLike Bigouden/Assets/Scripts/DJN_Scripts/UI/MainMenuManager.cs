using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator splashscreenAnimator;

    private void Start()
    {
        splashscreenAnimator.Play("load_splashscreen");
    }

    #region BUTTONS

    

    #endregion
}
