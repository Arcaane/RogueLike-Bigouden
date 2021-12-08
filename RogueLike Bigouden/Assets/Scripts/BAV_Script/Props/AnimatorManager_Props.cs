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
    [SerializeField] int endProjectorF;


    public void Start()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            pillarCount = t.pillarCount;
            projectorCount = t.projectorCount;
            rackCount = t.rackCount;
            endProjectorF = t.projectorCount - 1;
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
        foreach (ProjectorPropsProperties t in props)
        {
            //Get the privateFloat of Pillar;

            //Check the ID of the Pillar to setup the Function
            firstPillar = t.listOfPillar[firstPillarF];
            endPillar = t.listOfPillar[endProjectorF];
            if (t.animatorProjectorList == null)
            {
                endProjectorF = 0;
            }

            if (firstPillar.hurt && firstPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
            {
                if (t.animatorProjectorList != null)
                {
                    t.animatorProjectorList[firstPillarF].SetTrigger("Fall");
                    //Insert Particules System
                    Destroy(t.animatorProjectorList[firstPillarF].gameObject, 3f);
                    t.animatorProjectorList.RemoveAt(firstPillarF);

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

            /*
            if (firstPillar.hurt && firstPillar.incrementInt == t.lifeLaunchRackAnim && !destructPropsProjector1)
            {
                t.animatorRackList[endPillarF].SetTrigger("Fall");
                //Insert Particules System
                Destroy(t.animatorRackList[firstPillarF].gameObject, 3f);
                t.animatorRackList.RemoveAt(firstPillarF);
                endPillar = t.listOfPillar[endPillarF];
            }

            if (endPillar.hurt && endPillar.incrementInt >= t.lifeLaunchRackAnim && !destructPropsProjector2)
            {
                t.animatorRackList[endPillarF].SetTrigger("Fall");
                //Insert Particules System
                Destroy(t.animatorRackList[endPillarF].gameObject, 3f);
                t.animatorRackList.RemoveAt(firstPillarF);
                endPillar = t.listOfPillar[endPillarF];
            }
            */
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