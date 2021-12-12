using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemsManager : MonoBehaviour
{
    public Items[] items;
    public Items[] malus;
    public Items selectedMalus;
    public Items[] shop;
    private GameObject[] players;
    public List<Items> commonItems;

    public List<Items> rareItems;

    public List<Items> epicItems;

    public List<DropSystem> itemsInRoom;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        //ajouter tous les items dans le dossier sur la liste.
        items = Resources.LoadAll<Items>("Items");
        malus = Resources.LoadAll<Items>("Malus");
        shop = Resources.LoadAll<Items>("Shop");
        players = GameObject.FindGameObjectsWithTag("Player");
        
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

    private void Start()
    {
        itemsInRoom.AddRange(FindObjectsOfType<DropSystem>());
        selectedMalus = malus[UnityEngine.Random.Range(0, malus.Length)];
    }

    private void Update()
    {
        Malus();
    }

    private void Malus()
    {
        if (itemsInRoom.Count == 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Debug.Log("You take a malus.");
                players[i].GetComponent<Inventory>().items.Add(selectedMalus);
            }
        }
    }
}
