using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager instance;
    public GameObject player;
    
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
    public TextMeshProUGUI healthText;
    public Image energyBarImage;
    public TextMeshProUGUI moneyText;
    //insérer script statistique player

    [Header("Enemy HUD")] 
    public GameObject enemyUI;
    public GameObject enemyHealthBarObject;
    public Image healthBarEnemy;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyName;
    private GameObject lastEnemyHit;

    [Header("Test Information")] 
    [Range(0, 10)]
    public float currentHealth;
    public float maxHealth;
    [Range(0, 999)]
    public int currentMoney;
    [Range(0, 100)]
    public float currentEnergy;
    public float maxEnergy;
    
    public bool searchInventory;
    private PlayerStatsManager _playerStatsManager;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)
 
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player1UI.SetActive(true);
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        searchInventory = false;
        imageItemPanel = itemPanelParent.GetComponentsInChildren<Transform>();
        
        itemInformationPanel.SetActive(false);
        
        dialogueBox.SetActive(false);
        
        //trouver le script player statistiques
    }
    
    void UpdateItemPlayer()
    {
        if (inventory)
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
    }

    public void ItemPanelInformation()
    {
        itemInformationPanel.SetActive(true);

        if (inventory)
        {
            itemImage.sprite = inventory.itemOnTheFloor.image;
            itemDescriptionText.text = inventory.itemOnTheFloor.description;
            itemNameText.text = inventory.itemOnTheFloor.itemName;
            itemPriceText.text = inventory.itemOnTheFloor.price.ToString();
            rarityText.text = inventory.itemOnTheFloor.rarity.ToString();
        }
        
    }
    
    public void SearchPlayer(GameObject P)
    {
        player = P;
        Debug.Log(_playerStatsManager);
        _playerStatsManager = player.GetComponent<PlayerStatsManager>();
        Debug.Log(_playerStatsManager);
        searchInventory = true;
        player1UI.SetActive(true);
        Invoke(nameof(RefreshUI), 1);
    }

    public void RefreshUI()
    {
        Debug.Log(_playerStatsManager.maxLifePoint);
        Debug.Log(_playerStatsManager.lifePoint);
        
        float rlifePoint = (float)Math.Round(_playerStatsManager.lifePoint * 100f) / 100f;
        float rmaxLifePoint = (float)Math.Round(_playerStatsManager.maxLifePoint * 100f) / 100f;
        float rUltPoint = (float)Math.Round(_playerStatsManager.actualUltPoint * 100f) / 100f;
        float rmaxUltPoint = (float)Math.Round(_playerStatsManager.ultMaxPoint * 100f) / 100f;
        
        healthBarImage.fillAmount = rlifePoint / rmaxLifePoint;
        energyBarImage.fillAmount = rUltPoint / rmaxUltPoint;
        healthText.text = rlifePoint + "/" + rmaxLifePoint;
        moneyText.text = currentMoney.ToString();

        UpdateItemPlayer();
    }

    public void EnemyHealthBar()
    {
        //récupérer le dernier enemy touché dans le script de la collision enemy avec n'importe quel attaque (x,y,b) seulement si il reçoit des dégâts (dans tout les cas il est censé en prendre)
        
        if (lastEnemyHit)
        {
            enemyName.text = lastEnemyHit.name;
            //enemyHealthText.text = lastEnemyHit.healthActual + "/" + lastEnemyHit.maxHealth;
            //energyBarImage.fillAmount = lastEnemyHit.healthActual / lastEnemyHit.maxHealth;
        }
    }

    public void ClosePanel()
    {
        itemInformationPanel.SetActive(false);
    }
    
}
