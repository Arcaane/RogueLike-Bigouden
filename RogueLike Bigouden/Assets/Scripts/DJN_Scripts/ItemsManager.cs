using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Items[] items;
    // Start is called before the first frame update
    void Start()
    {
        //ajouter tous les items dans le dossier sur la liste.
        items = Resources.LoadAll<Items>("Items");
    }
}
