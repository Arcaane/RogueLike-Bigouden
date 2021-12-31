using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrickTrap : MonoBehaviour
{
    //--------------------TYPE OF PROPS WE INSTANTIATE--------------------//
    [SerializeField] public GameObject basicTrap;

    //Where we stock the prefab
    [SerializeField] public Transform trapFolder;
    [SerializeField] public List<GameObject> electrikPrefab;

    [SerializeField] public float spaceBetweenTrap;
    [SerializeField] public const float gridSnap = 0.03333334f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void InstantiatePrefab(int directionProps)
    {
        GameObject objTrap = Instantiate(basicTrap, trapFolder.transform, false);
        electrikPrefab.Add(objTrap);
        switch (directionProps)
        {
            //Top Spawn
            case 0:
                for (int i = 0; i < electrikPrefab.Count; i++)
                {
                    if (electrikPrefab.Count >= 1)
                    {

                        electrikPrefab[electrikPrefab.Count -1].gameObject.transform.position = new Vector3(
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.x,
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.y +
                            (gridSnap * spaceBetweenTrap),
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.z);
                    }
                }
                break;
            //Left Spawn
            case 1:
                for (int i = 0; i < electrikPrefab.Count; i++)
                {
                    if (electrikPrefab.Count >= 1)
                    {
                        electrikPrefab[electrikPrefab.Count -1].gameObject.transform.position = new Vector3(
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.x -
                            (gridSnap * spaceBetweenTrap),
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.y,
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.z);
                    }
                }
                break;
            //Rigt Spawn
            case 2:
                for (int i = 0; i < electrikPrefab.Count; i++)
                {
                    if (electrikPrefab.Count >= 1)
                    {
                        electrikPrefab[electrikPrefab.Count -1].gameObject.transform.position = new Vector3(
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.x +
                            (gridSnap * spaceBetweenTrap),
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.y,
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.z);
                    }
                }
                break;
            //Down Spawn
            case 3:
                for (int i = 0; i < electrikPrefab.Count; i++)
                {
                    if (electrikPrefab.Count >= 1)
                    {
                        electrikPrefab[electrikPrefab.Count -1].gameObject.transform.position = new Vector3(
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.x,
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.y -
                            (gridSnap * spaceBetweenTrap),
                            electrikPrefab[electrikPrefab.Count -2].gameObject.transform.position.z);
                    }
                }
                break;
        }
    }
}