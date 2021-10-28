using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using Random = System.Random;

public class DropSystem : MonoBehaviour
{
    private ItemsManager itemManager;
    private GameObject gameManager;
    private CircleCollider2D collider;
    public bool shop;
    public bool levelEnding;
    
    [Header("Generation Values")]
    public int roll;
    public int commonValue = 100;
    public int rareValue = 40;
    public int epicValue = 10;
    public Items itemSelect; //assigné un item dans la liste de l'ItemManager en fonction de son dropRate.

    private SpriteRenderer gameobjectSprite;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        itemManager = gameManager.GetComponent<ItemsManager>();
        gameobjectSprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        
        collider.enabled = false;

    }

    void Start()
    {
        
      
            ShopItemGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        EndLevelItemDrop();
    }

    void ShopItemGeneration()
    {
        if (shop)
        {
            collider.enabled = true;
            roll = UnityEngine.Random.Range(0, 100);
            Debug.Log(roll);

            if (roll < epicValue)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.red;
            }

            if (roll > epicValue && roll < rareValue)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.blue;
            }

            if (roll > rareValue && roll < commonValue)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.green;
            }
        }
    }
    
    private void EndLevelItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && levelEnding)
        {
            collider.enabled = true;
            roll = UnityEngine.Random.Range(0, 100);
            Debug.Log(roll);

            if (roll < epicValue)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.red;
            }

            if (roll > epicValue && roll < rareValue)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.blue;
            }

            if (roll > rareValue && roll < commonValue)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
                gameobjectSprite.sprite = itemSelect.image;
                gameobjectSprite.color = Color.green;
            }
        }
    }
}
