using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class DropSystem : MonoBehaviour
{
    private ItemsManager itemManager;
    private GameObject gameManager;
    private UIManager _uiManager;
    private CircleCollider2D collider;
    private WaveSpawner m_roomLoader;

    public bool shop;
    public bool levelEnding;

    [Header("Generation Values")] public int roll;
    public int commonValue = 100;
    public int rareValue = 40;
    public int epicValue = 10;
    public Items itemSelect; //assigné un item dans la liste de l'ItemManager en fonction de son dropRate.

    private SpriteRenderer gameobjectSprite;

    private PlayerAttribut player;
    private Inventory playerInventory;
    public bool playerOnIt;
    public UIManager refUI;
    private bool itemAlreadySpawn;

    // Start is called before the first frame update
    private void Awake()
    {
        itemManager = FindObjectOfType<ItemsManager>();
        gameobjectSprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        _uiManager = FindObjectOfType<UIManager>();
        refUI = _uiManager;

        itemAlreadySpawn = false;
        collider.enabled = false;
    }

    void Start()
    {
        ShopItemGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_roomLoader)
        {
            m_roomLoader = FindObjectOfType<WaveSpawner>();
        }
        
        if (!shop && m_roomLoader.State == WaveSpawner.SpawnState.FINISHED && !itemAlreadySpawn)
        {
            EndLevelItemDrop();
        }

        if (playerOnIt && Input.GetKeyDown(KeyCode.X) &&
            PlayerStatsManager.playerStatsInstance.money >= itemSelect.price) // PlayerStat Money
        {
            playerInventory.items.Add(itemSelect);
            PlayerStatsManager.playerStatsInstance.money -= itemSelect.price; // PlayerStat Money
            Destroy(gameObject);
            UIManager.instance.RefreshUI();
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

            if (other.GetComponent<PlayerAttribut>() != null)
            {
                player = other.GetComponent<PlayerAttribut>();
                playerInventory = other.GetComponent<Inventory>();
                player.canTakeItem = true;
                playerOnIt = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        _uiManager.itemInformationPanel.SetActive(false);
        if (other.GetComponent<PlayerAttribut>() != null)
        {
            playerInventory = null;
            player.canTakeItem = false;
            playerOnIt = false;
        }
    }
    
    private void Roll()
    {
        roll = UnityEngine.Random.Range(0, 100);
    }

    private void EndLevelItemDrop()
    {
        itemAlreadySpawn = true;
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