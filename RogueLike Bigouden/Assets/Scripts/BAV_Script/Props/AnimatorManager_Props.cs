using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager_Props : MonoBehaviour
{
    public List<ProjectorPropsProperties> props;


    public void Start()
    {
        
    }

    public void Update()
    {
        LaunchAnimation();
    }

    public void LateUpdate()
    {
        
    }

    public void LaunchAnimation()
    {
        
    }
}


[Serializable]
public class ProjectorPropsProperties
{
    public int pillarCount;
    public List<Props_EnvironnementManager> listOfPillar;
    public int projectorCount;
    public List<Animator> animatorProjectorList;
    public int rackCount;
    public List<Animator> animatorRackList;
}
