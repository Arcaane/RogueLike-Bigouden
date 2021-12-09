using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Controls;
using Random = System.Random;

public class DropSystem : MonoBehaviour
{
    private ItemsManager itemManager;
    private GameObject gameManager;
    private UIManager _uiManager;
    private CircleCollider2D collider;
    private RoomLoader m_roomLoader;
    
    public bool shop;
    public bool levelEnding;
    
    [Header("Generation Values")]
    public int roll;
    public int commonValue = 100;
    public int rareValue = 40;
    public int epicValue = 10;
    public Items itemSelect; //assigné un item dans la liste de l'ItemManager en fonction de son dropRate.

    private SpriteRenderer gameobjectSprite;

    private Inventory playerInventory;
    public bool playerOnIt;

    // Start is called before the first frame update
    private void Awake()
    {
        itemManager = FindObjectOfType<ItemsManager>();
        gameobjectSprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        _uiManager = FindObjectOfType<UIManager>();
        m_roomLoader = GetComponent<RoomLoader>();
        
        collider.enabled = false;
    }

    void Start()
    {
        ShopItemGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shop)
        {
            EndLevelItemDrop();
        }
        
        if (playerOnIt && Input.GetKeyDown(KeyCode.X) && playerInventory.currentMoney >= itemSelect.price) // PlayerStat Money
        {
            playerInventory.items.Add(itemSelect);
            playerInventory.currentMoney -= itemSelect.price; // PlayerStat Money
            Destroy(gameObject);
        }
        
    }

    void ShopItemGeneration()
    {
        if (shop)
        {
            Roll();
            collider.enabled = true;
            Debug.Log(roll);

            if (roll < epicValue)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.red;
            }
            else
            {
                //ShopItemGeneration();
            }

            if (roll > epicValue && roll < rareValue)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.blue;
            }
            else
            {
                //ShopItemGeneration();
            }

            if (roll > rareValue && roll < commonValue)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.green;
            }
            else
            {
                //ShopItemGeneration();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>()) // On peut enlever normalement
        {
            _uiManager.itemInformationPanel.SetActive(true);
            _uiManager.InformationPanel(itemSelect);
            
            //bool 
            playerInventory = other.GetComponent<Inventory>();
            playerOnIt = true;
            
        }

    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        _uiManager.itemInformationPanel.SetActive(false);
        playerInventory = null;
        playerOnIt = false;
    }

    private void Roll()
    {
        roll = UnityEngine.Random.Range(0, 100);
    }
    
    private void EndLevelItemDrop()
    {
        if (m_roomLoader._waveManager.GetComponent<WaveSpawner>().State == WaveSpawner.SpawnState.FINISHED)
        {
            Roll();
            collider.enabled = true;
            Debug.Log(roll);

            if (roll < epicValue)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.red;
            }
            else
            {
                //EndLevelItemDrop();
            }

            if (roll > epicValue && roll < rareValue)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.blue;
            }
            else
            {
                //EndLevelItemDrop();
            }

            if (roll > rareValue && roll < commonValue)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.green;
            }
            else
            {
                //EndLevelItemDrop();
            }
        }
    }
}
