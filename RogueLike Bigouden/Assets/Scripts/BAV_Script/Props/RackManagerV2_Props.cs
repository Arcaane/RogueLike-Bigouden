using System;
using System.Collections.Generic;
using UnityEngine;

public class RackManagerV2_Props : MonoBehaviour
{
    public List<ProjectorPropsPropertiesV2> props;

    //--------------------IF IS GENERATED (LIST GOT GAMEOBJECT)--------------------//
    [SerializeField] private bool isGeneretedAlready;

    //--------------------Props ENVIRONNEMENT REF--------------------//
    [SerializeField] private Props_EnvironnementManager firstPillar;
    [SerializeField] private Props_EnvironnementManager endPillar;

    //Rack Range
    private int firstRackF;

    //--------------------Projector Settings--------------------//
    [Header("Number of Projector to Instantiate")] [SerializeField]
    private bool isRackInXAxis;

    [SerializeField] int numberOfProjectorA;
    [SerializeField] int numberOfProjectorB;

    [SerializeField] int spaceBetweenProjector = 24;
    //Rajouter un random dans le Pop des projecteurs.


    [Header("Prefab to Instantiate")]
    //--------------------PREFAB--------------------//
    [SerializeField] private List<Transform> projFolderTransform;
    [SerializeField] private List<GameObject> projecteurList;

    //Projector Range
    [SerializeField] int firstProjectorF = 0;
    [SerializeField] int endProjectorF;

    [Header("Update Data of the Props for Debug")]
    //--------------------DATA--------------------//
    [SerializeField]
    private bool updateData;

    [SerializeField] private Vector2Int sizeXY_offsetZW;
    [SerializeField] private Vector2Int adjustPos_AProj_XY;
    [SerializeField] private Vector2Int adjustPos_BProj_XY;

    //Private Value
    //--------------------DATA FOR THE RACK--------------------//
    private Vector2 boxColliderRackSize;
    private Vector2 boxColliderRackSizeData;
    private Vector2 spriteRendererRackSize;
    private Vector2 spriteRendererRackSizeData;

    private Vector2 originPosRack;

    //--------------------DATA OF THE ORIGIN OF THE PROJECTOR--------------------//
    private Vector2 originProjectorA;
    private Vector2 originProjectorB;

    private Vector3 posFirstPilarForProjA;
    private Vector3 posEndtPilarForProjB;

    private const float gridSnap = 0.03333334f;

    public void Start()
    {
        if (!isGeneretedAlready)
        {
            LaunchGeneration();
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

    void LaunchGeneration()
    {
        foreach (ProjectorPropsPropertiesV2 t in props)
        {
            firstPillar = t.listOfPillar[0];
            endPillar = t.listOfPillar[1];
            boxColliderRackSizeData = t.animatorRackList[0].GetComponent<BoxCollider2D>().size;
            spriteRendererRackSizeData = t.animatorRackList[0].GetComponent<SpriteRenderer>().size;
            originPosRack = t.animatorRackList[0].gameObject.transform.position;

            #region Instantiate Number of Projector from the first Pillar

            //--------------------INSTANTIATE NUMBER OF PROJECTOR A--------------------//
            for (int i = 0; i < numberOfProjectorA; i++)
            {
                GameObject obj = Instantiate(projecteurList[0], projFolderTransform[0].transform, true);
                t.animatorProjectorList_Left.Add(obj);
                posFirstPilarForProjA = firstPillar.transform.position;
                //Set the origin point of the Projector
                //If the rack has to be Horizontal
                if (isRackInXAxis)
                {
                    t.animatorProjectorList_Left[0].gameObject.transform.position = new Vector3(
                        posFirstPilarForProjA.x + (1.40131f / 4),
                        posFirstPilarForProjA.y + 0.1333334f,
                        posFirstPilarForProjA.z + 0);
                }
                else
                {
                    t.animatorProjectorList_Left[0].gameObject.transform.position = new Vector3(
                        posFirstPilarForProjA.x,
                        posFirstPilarForProjA.y + 0.1333334f,
                        posFirstPilarForProjA.z + 0);
                }

                //Store the origin Position
                originProjectorA = t.animatorProjectorList_Left[0].gameObject.transform.position;

                //Set the other position from the original position
                //If the rack has to be Horizontal
                if (isRackInXAxis)
                {
                    t.animatorProjectorList_Left[i].gameObject.transform.position = new Vector3(
                        t.animatorProjectorList_Left[0].gameObject.transform.position.x +
                        (i * (gridSnap * spaceBetweenProjector)),
                        t.animatorProjectorList_Left[0].gameObject.transform.position.y,
                        t.animatorProjectorList_Left[0].gameObject.transform.position.z);
                }
                //If the rack has to be Vertical
                else
                {
                    t.animatorProjectorList_Left[i].gameObject.transform.position = new Vector3(
                        t.animatorProjectorList_Left[0].gameObject.transform.position.x,
                        t.animatorProjectorList_Left[0].gameObject.transform.position.y +
                        (i * (gridSnap * spaceBetweenProjector)),
                        t.animatorProjectorList_Left[0].gameObject.transform.position.z);
                }
            }

            #endregion

            #region Instantiate Number of Projector from the End Pillar

            //--------------------INSTANTIATE NUMBER OF PROJECTOR B--------------------//
            for (int i = 0; i < numberOfProjectorB; i++)
            {
                GameObject obj = Instantiate(projecteurList[1], projFolderTransform[1].transform, true);
                t.animatorProjectorList_Right.Add(obj);
                posEndtPilarForProjB = endPillar.transform.position;
                //Set the origin point of the Projector
                //If the rack has to be Horizontal
                if (isRackInXAxis)
                {
                    t.animatorProjectorList_Right[0].gameObject.transform.position = new Vector3(
                        posEndtPilarForProjB.x - (1.40131f / 4),
                        posEndtPilarForProjB.y + 0.1333334f,
                        posEndtPilarForProjB.z + 0);
                }
                else
                {
                    t.animatorProjectorList_Right[0].gameObject.transform.position = new Vector3(
                        posEndtPilarForProjB.x,
                        posEndtPilarForProjB.y + 0.1333334f,
                        posEndtPilarForProjB.z + 0);
                }

                //Store the origin Position
                originProjectorB = t.animatorProjectorList_Right[0].gameObject.transform.position;

                //Set the other position from the original position
                //If the rack has to be Horizontal
                if (isRackInXAxis)
                {
                    t.animatorProjectorList_Right[i].gameObject.transform.position = new Vector3(
                        t.animatorProjectorList_Right[0].gameObject.transform.position.x -
                        (i * (gridSnap * spaceBetweenProjector)),
                        t.animatorProjectorList_Right[0].gameObject.transform.position.y,
                        t.animatorProjectorList_Right[0].gameObject.transform.position.z);
                }

                //If the rack has to be Vertical
                else
                {
                    t.animatorProjectorList_Right[i].gameObject.transform.position = new Vector3(
                        t.animatorProjectorList_Right[0].gameObject.transform.position.x,
                        t.animatorProjectorList_Right[0].gameObject.transform.position.y -
                        (i * (gridSnap * spaceBetweenProjector)),
                        t.animatorProjectorList_Right[0].gameObject.transform.position.z);
                }
            }

            #endregion

            /*
            //--------------------CHANGE THE SIZE OF THE RACK BETWEEN TWO BEAM--------------------//
            t.animatorRackList[0].transform.position = new Vector3(
                originPosRack.x + ((posEndtPilarForProjB.x - posFirstPilarForProjA.x)/2),
                //originPosRack.y * (posEndtPilarForProjB.y - posEndtPilarForProjB.y),
                originPosRack.y ,
                0);
                */
        }
    }

    public void LaunchAnimationProjector()
    {
        foreach (ProjectorPropsPropertiesV2 t in props)
        {
            //Get the privateFloat of Pillar;
            //Check the ID of the Pillar to setup the Function
            //Projector--------------------------------------------------------------------
            if (t.animatorProjectorList_Left.Count == 0 && t.animatorProjectorList_Right.Count == 0)
            {
                return;
            }

            if (t.animatorProjectorList_Left.Count != 0)
            {
                if (firstPillar.hurt)
                {
                    switch (firstPillar.incrementFloat)
                    {
                        case 0:
                            break;

                        case var _ when (firstPillar.incrementFloat >= (t.lifeLaunchProjectorAnim - 1) && firstPillar.incrementFloat < (t.lifeLaunchProjectorAnim +1)):
                            t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - (numberOfProjectorA)]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - numberOfProjectorA]
                                    .gameObject, 3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count -
                                                                  numberOfProjectorA);
                            break;

                        case var _ when (firstPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 2) - 1) && firstPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 2) +1)):
                            t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 4]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 4].gameObject,
                                3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count - 4);
                            break;

                        case var _ when (firstPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 3) - 1) && firstPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 3) +1)):
                            t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 3]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 3].gameObject,
                                3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count - 3);
                            break;

                        case var _ when (firstPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 4) - 1) && firstPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 4) +1)):
                            t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 2]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 2].gameObject,
                                3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count - 2);
                            break;

                        case var _ when (firstPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 4) +1) && firstPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 4) +3)):
                            t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 1]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(t.animatorProjectorList_Left[t.animatorProjectorList_Left.Count - 1].gameObject,
                                3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count - 1);
                            break;

                        case var _ when (firstPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 5) -2)):
                            t.animatorProjectorList_Left[
                                    t.animatorProjectorList_Left.Count - t.animatorProjectorList_Left.Count]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Left[
                                    t.animatorProjectorList_Left.Count - t.animatorProjectorList_Left.Count].gameObject,
                                3f);
                            t.animatorProjectorList_Left.RemoveAt(t.animatorProjectorList_Left.Count -
                                                                  t.animatorProjectorList_Left.Count);
                            break;
                    }
                }
            }


            if (t.animatorProjectorList_Right.Count != 0)
            {
                if (endPillar.hurt)
                {
                    switch (endPillar.incrementFloat)
                    {
                        case 0:
                            break;
                        case var _ when (endPillar.incrementFloat >= (t.lifeLaunchProjectorAnim - 1) && endPillar.incrementFloat < (t.lifeLaunchProjectorAnim +1)):

                            t.animatorProjectorList_Right[
                                    t.animatorProjectorList_Right.Count - (numberOfProjectorB)]
                                .GetComponent<Animator>()
                                .SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[
                                    t.animatorProjectorList_Right.Count - numberOfProjectorB].gameObject, 3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count -
                                                                   numberOfProjectorB);

                            break;
                        case var _ when (endPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 2) - 1) && endPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 2) +1)):

                            t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 4]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 4]
                                    .gameObject,
                                3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count - 4);

                            break;
                        case var _ when (endPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 3) - 1) && endPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 3) +1)):

                            t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 3]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 3]
                                    .gameObject,
                                3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count - 3);

                            break;
                        case var _ when (endPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 4) - 1) && endPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 4) +1)):

                            t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 2]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 2]
                                    .gameObject,
                                3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count - 2);

                            break;
                        case var _ when (endPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 4) +1) && endPillar.incrementFloat < ((t.lifeLaunchProjectorAnim * 4) +3)):

                            t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 1]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[t.animatorProjectorList_Right.Count - 1]
                                    .gameObject,
                                3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count - 1);

                            break;
                        case var _ when (endPillar.incrementFloat >= ((t.lifeLaunchProjectorAnim * 5) -2)):

                            t.animatorProjectorList_Right[
                                    t.animatorProjectorList_Right.Count - t.animatorProjectorList_Right.Count]
                                .GetComponent<Animator>().SetTrigger("Fall");
                            //Insert Particules System
                            Destroy(
                                t.animatorProjectorList_Right[
                                        t.animatorProjectorList_Right.Count -
                                        t.animatorProjectorList_Right.Count]
                                    .gameObject, 3f);
                            t.animatorProjectorList_Right.RemoveAt(t.animatorProjectorList_Right.Count -
                                                                   t.animatorProjectorList_Right.Count);

                            break;
                    }
                }
            }
        }
    }

    public void LaunchRackAnimations()
    {
        foreach (ProjectorPropsPropertiesV2 t in props)
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
                    if (t.animatorProjectorList_Right.Count != 0)
                    {
                        for (int i = 0; i < t.animatorProjectorList_Right.Count; i++)
                        {
                            t.animatorProjectorList_Right[i].gameObject.GetComponent<Animator>().SetTrigger("Fall");
                            Destroy(t.animatorProjectorList_Right[i], 3f);
                            t.animatorProjectorList_Right.RemoveAt(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < t.animatorProjectorList_Right.Count; i++)
                        {
                            Destroy(t.animatorProjectorList_Right[i].gameObject);
                            t.animatorProjectorList_Right.Clear();
                        }
                    }

                    t.listOfPillar.RemoveAt(t.listOfPillar.Count - t.listOfPillar.Count);
                    if (t.animatorRackList.Count != 0 || t.animatorProjectorList_Right.Count == 0)
                    {
                        t.animatorRackList[0].GetComponent<Animator>().SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorRackList[0].gameObject, 3f);
                        t.animatorRackList.RemoveAt(0);
                    }
                }

                if (endPillar.lifePoint < 0
                    && firstPillar.lifePoint > 0)
                {
                    t.listOfPillar.RemoveAt(t.listOfPillar.Count - 1);

                    if (t.animatorProjectorList_Left.Count != 0)
                    {
                        for (int i = 0; i < t.animatorProjectorList_Left.Count; i++)
                        {
                            t.animatorProjectorList_Left[i].gameObject.GetComponent<Animator>().SetTrigger("Fall");
                            Destroy(t.animatorProjectorList_Left[i], 3f);
                            t.animatorProjectorList_Left.RemoveAt(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < t.animatorProjectorList_Left.Count; i++)
                        {
                            Destroy(t.animatorProjectorList_Left[i].gameObject);
                            t.animatorProjectorList_Left.Clear();
                        }
                    }

                    if (t.animatorRackList.Count != 0 || t.animatorProjectorList_Left.Count == 0)
                    {
                        t.animatorRackList[0].GetComponent<Animator>().SetTrigger("Fall");
                        //Insert Particules System
                        Destroy(t.animatorRackList[0].gameObject, 3f);
                        t.animatorRackList.RemoveAt(0);
                    }
                }
            }
        }

//If one Rack and when a pillar si destroy

//For the futur Me,
//Implement a delay when a first pillar is destroy => launch if(delayIncrement >= DelayTimer) => then Destroy It 
    }


    public void UpdateData()
    {
        foreach (ProjectorPropsPropertiesV2 t in props)
        {
            boxColliderRackSize = new Vector2(boxColliderRackSizeData.x * sizeXY_offsetZW.x,
                boxColliderRackSizeData.y * sizeXY_offsetZW.y);
            spriteRendererRackSize = new Vector2(spriteRendererRackSizeData.x * sizeXY_offsetZW.x,
                spriteRendererRackSizeData.y * sizeXY_offsetZW.y);

            //--------------------UPDATE IN REAL TIME RACK COLLIDER && RACK COLLIDER--------------------//
            t.animatorRackList[0].GetComponent<BoxCollider2D>().size = new Vector2(
                boxColliderRackSize.x,
                boxColliderRackSize.y);
            t.animatorRackList[0].GetComponent<SpriteRenderer>().size = new Vector2(
                spriteRendererRackSize.x,
                spriteRendererRackSize.y);


            //--------------------UPDATE IN REAL TIME POSITION OF PROJECTOR A--------------------//
            for (int i = 0; i < numberOfProjectorA; i++)
            {
                t.animatorProjectorList_Left[0].gameObject.transform.position = new Vector3(
                    originProjectorA.x +
                    (adjustPos_AProj_XY.x * gridSnap),
                    originProjectorA.y +
                    (adjustPos_AProj_XY.y * gridSnap),
                    0);
                t.animatorProjectorList_Left[i].gameObject.transform.position = new Vector3(
                    t.animatorProjectorList_Left[0].gameObject.transform.position.x +
                    (i * (gridSnap * spaceBetweenProjector)),
                    t.animatorProjectorList_Left[0].gameObject.transform.position.y,
                    t.animatorProjectorList_Left[0].gameObject.transform.position.z);
            }

            //--------------------UPDATE IN REAL TIME POSITION OF PROJECTOR B--------------------//
            for (int i = 0; i < numberOfProjectorB; i++)
            {
                t.animatorProjectorList_Right[0].gameObject.transform.position = new Vector3(
                    originProjectorB.x +
                    (adjustPos_BProj_XY.x * gridSnap),
                    originProjectorB.y +
                    (adjustPos_BProj_XY.y * gridSnap),
                    0);
                t.animatorProjectorList_Right[i].gameObject.transform.position = new Vector3(
                    t.animatorProjectorList_Right[0].gameObject.transform.position.x -
                    i * (gridSnap * spaceBetweenProjector),
                    t.animatorProjectorList_Right[0].gameObject.transform.position.y,
                    t.animatorProjectorList_Right[0].gameObject.transform.position.z);
            }
        }
    }

    public void Reset()
    {
        sizeXY_offsetZW = new Vector2Int(1, 1);
    }
}

[Serializable]
public class ProjectorPropsPropertiesV2
{
    //List des piliers pour ce props
    public List<Props_EnvironnementManager> listOfPillar;

    //--------------------Projector--------------------//
    //Projector Life Before It fall
    public int lifeLaunchProjectorAnim;

    //Projector list when we hurt the left pillar
    public List<GameObject> animatorProjectorList_Left;

    //Projector list when we hurt the right pillar
    public List<GameObject> animatorProjectorList_Right;

    //--------------------RACK----------------------//
    //Rack list when we destroy one Pillar
    public List<GameObject> animatorRackList;
}