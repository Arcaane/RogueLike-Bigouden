using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager_Props : MonoBehaviour
{
    public List<Props_EnvironnementManager> propsEnviro;
    public List<Animator> animatorList;

    //private
    private float incrementDamageObj1;
    private float incrementDamageObj2;
    

    public void Update()
    {
        LaunchAnimation();
    }

    public void LateUpdate()
    {
        incrementDamageObj1 = propsEnviro[0].incrementFloat;
        incrementDamageObj2 = propsEnviro[1].incrementFloat;
    }

    public void LaunchAnimation()
    {
        if (propsEnviro[0].hurt)
        {
            animatorList[0].SetFloat("Fall", incrementDamageObj1);
        }
        
        if (propsEnviro[1].hurt)
        {
            animatorList[1].SetFloat("Fall", incrementDamageObj2);
        }
    }
}
