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
    [SerializeField] bool destructPropsProjector1;
    [SerializeField] bool destructPropsProjector2;


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
        destructPropsProjector1 = props[0].animatorProjectorList[firstPillarF].gameObject
            .GetComponent<Props_EnvironnementManager>().isDestruct;
        destructPropsProjector2 = props[0].animatorProjectorList[endPillarF].gameObject
            .GetComponent<Props_EnvironnementManager>().isDestruct;


        foreach (ProjectorPropsProperties t in props)
        {
            //Get the privateFloat of Pillar;

            //Check the ID of the Pillar to setup the Function
            firstPillar = t.listOfPillar[firstPillarF];
            endPillar = t.listOfPillar[endPillarF];

            if (firstPillar.hurt && firstPillar.incrementInt == t.lifeLaunchProjectorAnim && !destructPropsProjector1)
            {
                t.animatorProjectorList[firstPillarF].SetTrigger("Fall");
                //Insert Particules System
                Destroy(t.animatorProjectorList[firstPillarF].gameObject, 3f);
                t.animatorProjectorList.RemoveAt(firstPillarF);
                if (endPillarF != 0)
                {
                    endPillarF--;
                }
            }

            if (endPillar.hurt && endPillar.incrementInt == t.lifeLaunchProjectorAnim && !destructPropsProjector2)
            {

                t.animatorProjectorList[endPillarF].SetTrigger("Fall");
                //Insert Particules System
                Destroy(t.animatorProjectorList[endPillarF].gameObject, 3f);
                t.animatorProjectorList.RemoveAt(endPillarF);
                if (endPillarF != 0)
                {
                    endPillarF--;
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