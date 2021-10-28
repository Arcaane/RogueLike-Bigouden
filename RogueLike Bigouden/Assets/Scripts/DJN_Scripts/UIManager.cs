using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Inventory inventory;
    public Transform[] imageItemPanel;
    public Transform itemPanelParent;
    
    [Header("Item Information Panel")] 
    public GameObject itemInformationPanel;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI rarityText;
    public Image itemImage;
    
    
    [Header("DialogueBox")] 
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    
    [Header("Player1 HUD")] 
    public GameObject player1UI;
    public Image healthBarImage;
    public Image energyBarImage;
    public TextMeshProUGUI moneyText;

    [Header("Test Information")] 
    [Range(0, 10)]
    public float currentHealth;
    public float maxHealth;
    [Range(0, 999)]
    public int currentMoney;
    [Range(0, 100)]
    public float currentEnergy;
    public float maxEnergy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player1UI.SetActive(true);
        TestingFunction();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        imageItemPanel = itemPanelParent.GetComponentsInChildren<Transform>();
        itemInformationPanel.SetActive(false);
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        energyBarImage.fillAmount = currentEnergy / maxEnergy;
        moneyText.text = currentMoney.ToString();
        
        UpdateItemPlayer();
        
    }

    void UpdateItemPlayer()
    {
        foreach (Transform itemsImage in imageItemPanel)
        {
            for (int i = 1; i < imageItemPanel.Length; i++)
            {
                if(imageItemPanel.Length > inventory.items.Count)
                    imageItemPanel[i].GetComponent<Image>().enabled = false;
                
            }

            for (int i = 0; i < inventory.items.Count; i++)
            {
                imageItemPanel[i +1].GetComponent<Image>().enabled = true;
                imageItemPanel[i + 1].GetComponent<Image>().sprite = inventory.items[i].image;
            }
        }
    }

    public void ItemPanelInformation()
    {
        itemInformationPanel.SetActive(true);

        itemImage.sprite = inventory.itemOnTheFloor.image;
        itemDescriptionText.text = inventory.itemOnTheFloor.description;
        itemNameText.text = inventory.itemOnTheFloor.itemName;
        itemPriceText.text = inventory.itemOnTheFloor.price.ToString();
        rarityText.text = inventory.itemOnTheFloor.rarity.ToString();
    }

    public void ClosePanel()
    {
        itemInformationPanel.SetActive(false);
    }
    
    void TestingFunction()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
    }
}
