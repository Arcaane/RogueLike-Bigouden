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


    //Rack Range
    [SerializeField] int firstBeamF;
    [SerializeField] int endBeamF;


    //Projector Range
    [SerializeField] int firstProjectorF;
    [SerializeField] int endProjectorF;


    public void Start()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            pillarCount = t.pillarCount;
            projectorCount = t.projectorCount;
            rackCount = t.rackCount;
            //Projector
            endProjectorF = t.projectorCount - 1;
            //Beam for the Rack
            endBeamF = t.pillarCount - 1;
        }
    }

    public void FixedUpdate()
    {
        LaunchAnimationProjector();
    }

    public void LateUpdate()
    {
    }

    public void LaunchAnimationProjector()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            //Get the privateFloat of Pillar;

            //Check the ID of the Pillar to setup the Function
            firstPillar = t.listOfPillar[firstProjectorF];
            endPillar = t.listOfPillar[endProjectorF];
            if (t.animatorProjectorList == null)
            {
                endProjectorF = 0;
            }

            //Projector--------------------------------------------------------------------
            if (firstPillar.hurt && firstPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
            {
                if (t.animatorProjectorList != null)
                {
                    t.animatorProjectorList[firstProjectorF].SetTrigger("Fall");
                    //Insert Particules System
                    Destroy(t.animatorProjectorList[firstProjectorF].gameObject, 3f);
                    t.animatorProjectorList.RemoveAt(firstProjectorF);

                    if (endProjectorF != 0)
                    {
                        endProjectorF--;
                    }
                }
            }

            if (endPillar.hurt && endPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
            {
                if (t.animatorProjectorList != null)
                {
                    t.animatorProjectorList[endProjectorF].SetTrigger("Fall");
                    //Insert Particules System
                    Destroy(t.animatorProjectorList[endProjectorF].gameObject, 3f);
                    t.animatorProjectorList.RemoveAt(endProjectorF);

                    if (endProjectorF != 0)
                    {
                        endProjectorF--;
                    }
                }
            }
        }
    }

    /*
    public void LaunchRackAnimations()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            if (t.animatorProjectorList == null)
            {
                firstPillar = t.listOfPillar[firstBeamF];
                endPillar = t.listOfPillar[endBeamF];

                if (firstPillar.isDestruct || endPillar.isDestruct)
                {
                    //If one Rack and when a pillar si destroy
                    t.animatorProjectorList[firstBeamF].SetTrigger("Fall");
                    //Insert Particules System
                    Destroy(t.animatorRackList[firstBeamF].gameObject, 3f);
                    t.animatorRackList.RemoveAt(firstBeamF);
                    endPillar = t.listOfPillar[firstBeamF];
                    
                    //For the futur Me,
                    //Implement a delay when a first pillar is destroy => launch if(delayIncrement >= DelayTimer) => then Destroy It 
                }
            }
        }
    }
    */
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