using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Items[] items;

    public List<Items> commonItems;

    public List<Items> rareItems;

    public List<Items> epicItems;
    // Start is called before the first frame update
    void Awake()
    {
        //ajouter tous les items dans le dossier sur la liste.
        items = Resources.LoadAll<Items>("Items");

        foreach (Items e in items)
        {
            if (e.rarity == Items.Rarity.Commun)
            {
                commonItems.Add(e);
            }
            
            if (e.rarity == Items.Rarity.Rare)
            {
                rareItems.Add(e);
            }
            
            if (e.rarity == Items.Rarity.Epic)
            {
                epicItems.Add(e);
            }
        }

    }
    
    
}
