using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackManager_Props : MonoBehaviour
{
    public List<ProjectorPropsProperties> props;

    //private----------
    //Props Environnement----------
    [SerializeField] private Props_EnvironnementManager firstPillar;
    [SerializeField] private Props_EnvironnementManager endPillar;

    //Rack Range
    [SerializeField] int firstRackF;


    //Projector Range
    [SerializeField] int firstProjectorF = 0;
    [SerializeField] int endProjectorF;

    //--------------------DATA--------------------//
    [SerializeField] private bool updateData;
    [SerializeField] private Vector2Int sizeXY_offsetZW;

    //Private Value
    private Vector2 boxColliderRackSize;
    private Vector2 boxColliderRackSizeData;
    private Vector2 spriteRendererRackSize;
    private Vector2 spriteRendererRackSizeData;


    public void Start()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            //Projector
            //Beam for the Rack
            firstPillar = t.listOfPillar[0];
            endPillar = t.listOfPillar[1];
            boxColliderRackSizeData = t.animatorRackList[0].GetComponent<BoxCollider2D>().size;
            spriteRendererRackSizeData = t.animatorRackList[0].GetComponent<SpriteRenderer>().size;
        }
    }

    public void FixedUpdate()
    {
        LaunchAnimationProjector();
        LaunchRackAnimations();
    }

    public void Update()
    {
        if (updateData)
        {
            UpdateData();
        }
    }

    public void UpdateData()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            boxColliderRackSize = new Vector2(boxColliderRackSizeData.x * sizeXY_offsetZW.x,
                boxColliderRackSizeData.y * sizeXY_offsetZW.y);
            spriteRendererRackSize = new Vector2(spriteRendererRackSizeData.x * sizeXY_offsetZW.x,
                spriteRendererRackSizeData.y * sizeXY_offsetZW.y);

            t.animatorRackList[0].GetComponent<BoxCollider2D>().size = new Vector2( 
                boxColliderRackSize.x,
                boxColliderRackSize.y);
            t.animatorRackList[0].GetComponent<SpriteRenderer>().size = new Vector2( 
                spriteRendererRackSize.x,
                spriteRendererRackSize.y);
        }
    }

    public void LaunchAnimationProjector()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            //Get the privateFloat of Pillar;
            //Check the ID of the Pillar to setup the Function
            //Projector--------------------------------------------------------------------
            if (t.animatorProjectorList_Left.Count == 0 && t.animatorProjectorList_Right.Count == 0)
            {
                Debug.Log("Je Suis Null");
                return;
            }

            if (t.animatorProjectorList_Left.Count != 0)
            {
                if (firstPillar.hurt)
                {
                    if (firstPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
                    {
                        t.animatorProjectorList_Left[firstProjectorF].SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorProjectorList_Left[firstProjectorF].gameObject, 3f);
                        t.animatorProjectorList_Left.RemoveAt(firstProjectorF);

                        if (endProjectorF != 0)
                        {
                            endProjectorF--;
                        }
                    }
                }
            }

            if (t.animatorProjectorList_Right.Count != 0)
            {
                if (endPillar.hurt)
                {
                    if (endPillar.incrementFloat >= t.lifeLaunchProjectorAnim)
                    {
                        Debug.Log("Coucou");
                        t.animatorProjectorList_Right[firstProjectorF].SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorProjectorList_Right[firstProjectorF].gameObject, 3f);
                        t.animatorProjectorList_Right.RemoveAt(firstProjectorF);

                        if (endProjectorF != 0)
                        {
                            endProjectorF--;
                        }
                    }
                }
            }
        }
    }

    public void LaunchRackAnimations()
    {
        foreach (ProjectorPropsProperties t in props)
        {
            if (t.listOfPillar.Count == 0)
            {
                return;
            }

            if (t.listOfPillar.Count != 0)
            {
                if (firstPillar.lifePoint < 0
                    && endPillar.lifePoint > 0)
                {
                    t.listOfPillar.RemoveAt(t.listOfPillar.Count - t.listOfPillar.Count);
                    if (t.animatorRackList.Count != 0)
                    {
                        t.animatorRackList[firstRackF].GetComponent<Animator>().SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorRackList[firstRackF].gameObject, 3f);
                        t.animatorRackList.RemoveAt(firstRackF);
                    }
                }

                if (endPillar.lifePoint < 0
                    && firstPillar.lifePoint > 0)
                {
                    t.listOfPillar.RemoveAt(t.listOfPillar.Count - 1);
                    if (t.animatorRackList.Count != 0)
                    {
                        t.animatorRackList[firstRackF].GetComponent<Animator>().SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorRackList[firstRackF].gameObject, 3f);
                        t.animatorRackList.RemoveAt(firstRackF);
                    }
                }
            }
        }

//If one Rack and when a pillar si destroy

//For the futur Me,
//Implement a delay when a first pillar is destroy => launch if(delayIncrement >= DelayTimer) => then Destroy It 
    }
}

[Serializable]
public class ProjectorPropsProperties
{
    //List des piliers pour ce props
    public List<Props_EnvironnementManager> listOfPillar;

    //--------------------Projector--------------------//
    //Projector Life Before It fall
    public int lifeLaunchProjectorAnim;

    //Projector list when we hurt the left pillar
    public List<Animator> animatorProjectorList_Left;

    //Projector list when we hurt the right pillar
    public List<Animator> animatorProjectorList_Right;

    //--------------------RACK----------------------//
    //Rack list when we destroy one Pillar
    public List<GameObject> animatorRackList;
}