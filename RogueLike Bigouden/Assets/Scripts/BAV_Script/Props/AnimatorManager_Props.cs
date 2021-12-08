using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager_Props : MonoBehaviour
{
    public List<ProjectorPropsProperties> props;

    //private----------
    //Props Environnement----------
    [SerializeField] private Props_EnvironnementManager firstPillar;
    [SerializeField] private Props_EnvironnementManager endPillar;

    [SerializeField] int projectorCount;
    [SerializeField] int pillarCount;
    [SerializeField] int rackCount;
    [SerializeField] int firstPillarF;
    [SerializeField] int endPillarF;


    public void Start()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            pillarCount = t.pillarCount;
            projectorCount = t.projectorCount;
            rackCount = t.rackCount;
            endPillarF = t.pillarCount - 1;
        }
    }

    public void FixedUpdate()
    {
        LaunchAnimation();
    }

    public void LateUpdate()
    {
    }

    public void LaunchAnimation()
    {
        firstPillar = null;
        endPillar = null;
        foreach (ProjectorPropsProperties t in props)
        {
            //Get the privateFloat of Pillar;

            //Check the ID of the Pillar to setup the Function
            firstPillar = t.listOfPillar[firstPillarF];
            endPillar = t.listOfPillar[endPillarF];

            if (firstPillar.hurt && firstPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
            {
                t.animatorProjectorList[firstPillarF].SetTrigger("Fall");
            }

            if (endPillar.hurt && endPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
            {
                t.animatorProjectorList[endPillarF].SetTrigger("Fall");
            }

            if (firstPillar.hurt && firstPillar.incrementFloat >= t.lifeLaunchRackAnim)
            {
                t.animatorRackList[endPillarF].SetTrigger("Fall");
            }
            
            if (endPillar.hurt && endPillar.incrementFloat >= t.lifeLaunchRackAnim)
            {
                t.animatorRackList[endPillarF].SetTrigger("Fall");
            }
        }
    }
}


[Serializable]
public class ProjectorPropsProperties
{
    public int pillarCount;
    public List<Props_EnvironnementManager> listOfPillar;
    public int lifeLaunchProjectorAnim;
    public int projectorCount;
    public List<Animator> animatorProjectorList;
    public int lifeLaunchRackAnim;
    public int rackCount;
    public List<Animator> animatorRackList;
}