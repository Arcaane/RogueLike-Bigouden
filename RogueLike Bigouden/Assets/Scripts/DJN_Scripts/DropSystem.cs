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
    private InventoryManager inventoryM;
    
    [Header("Generation Values")]
    public int roll;
    public int commonValue = 100;
    public int rareValue = 40;
    public int epicValue = 10;
    public Items itemSelect; //assigné un item dans la liste de l'ItemManager en fonction de son dropRate.

    [Header("GameObject Design")]
    public SpriteRenderer gameobjectSprite;
    public Light2D light;

    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image image;
    public GameObject itemPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        itemManager = gameManager.GetComponent<ItemsManager>();
        gameobjectSprite = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
        inventoryM = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();

        light.intensity = 0;
        itemPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roll = UnityEngine.Random.Range(0, 100);
            Debug.Log(roll);
            light.intensity = 1;
            
            if (roll < epicValue)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
                gameobjectSprite.sprite = itemSelect.image;
                light.color = Color.yellow;

                
            }

            if (roll > epicValue && roll < rareValue)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
                gameobjectSprite.sprite = itemSelect.image;
                light.color = Color.blue;
            }

            if (roll > rareValue && roll < commonValue)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
                gameobjectSprite.sprite = itemSelect.image;
                light.color = Color.green;
            }
            
        }

        
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && itemSelect)
        {
            itemPanel.SetActive(true);
            nameText.text = itemSelect.itemName;
            descriptionText.text = itemSelect.description;
            image.sprite = itemSelect.image;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                inventoryM.items.Add(itemSelect);
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            itemPanel.SetActive(false);
            
        }
    }
}
