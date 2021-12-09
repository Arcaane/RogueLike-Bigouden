using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject player;

    private Inventory inventory;
    public Transform[] imageItemPanel;
    public Transform itemPanelParent;

    [Header("DialogueBox")] public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Common UI")] 
    public GameObject itemInformationPanel;

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemPriceText;
    public Image itemIconImage;
    public TextMeshProUGUI itemRarityText;

    [Header("Player1 HUD")] public GameObject player1UI;
    public Image p1_healthBarImg;
    public TextMeshProUGUI p1_healthText;
    public Image p1_energyBarImage;

    public TextMeshProUGUI p1_moneyText;
    //insérer script statistique player

    [Header("Player2 HUD")] public GameObject player2UI;
    public Image p2_healthBarImg;
    public TextMeshProUGUI p2_healthText;
    public Image p2_energyBarImage;
    public TextMeshProUGUI p2_moneyText;

    [Header("Enemy HUD")] public GameObject enemyUI;
    public GameObject enemyHealthBarObject;
    public Image healthBarEnemy;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyName;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused;
    [SerializeField] private GameObject blur;

    [Header("Settings")] 
    public GameObject settingPanel;

    public AudioMixer masterMixer;

    [Header("Test Information")] [Range(0, 10)]
    public float currentHealth;

    public float maxHealth;
    [Range(0, 999)] public int currentMoney;
    [Range(0, 100)] public float currentEnergy;
    public float maxEnergy;
    
    

    public bool searchInventory;
    private PlayerStatsManager _playerStatsManager;
    public List<GameObject> playerList;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        searchInventory = false;
        dialogueBox.SetActive(false);
        isPaused = false;
        pauseMenu.SetActive(false);
        
        itemInformationPanel.SetActive(false);

        //trouver le script player statistiques

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        
        Pause();
        UpdateItemPlayer();
    }

    private void Pause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            blur.SetActive(true);
            Time.timeScale = 0;
        }

        if (!isPaused)
        {
            pauseMenu.SetActive(false);
            blur.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void UpdateItemPlayer()
    {
        //ecart de 55 sur l'axe x
        imageItemPanel = itemPanelParent.GetComponentsInChildren<Transform>();

        if (inventory)
        {
            foreach (Transform itemsImage in imageItemPanel)
            {
                for (int i = 1; i < imageItemPanel.Length; i++)
                {
                    if (imageItemPanel.Length > inventory.items.Count)
                        imageItemPanel[i].GetComponent<Image>().enabled = false;
                }

                for (int i = 0; i < inventory.items.Count; i++)
                {
                    imageItemPanel[i + 1].GetComponent<Image>().enabled = true;
                    imageItemPanel[i + 1].GetComponent<Image>().sprite = inventory.items[i].image;
                }
            }
        }
    }

    public void InformationPanel(Items items)
    {
        itemIconImage.sprite = items.image;
        itemNameText.text = items.itemName;
        itemDescriptionText.text = items.description;
        itemPriceText.text = items.price.ToString();
        itemRarityText.text = items.rarity.ToString();
    }

    public void SearchPlayer(GameObject P)
    {
        player = P;
        playerList.Add(player);

        foreach (GameObject pGameObject in playerList)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (i == 0)
                {
                    player1UI.SetActive(true);
                }

                if (i == 1)
                {
                    player2UI.SetActive(true);
                }
            }
        }

        Invoke(nameof(RefreshUI), 1);
    }

    public void RefreshUI()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            _playerStatsManager = playerList[i].GetComponent<PlayerStatsManager>();
            inventory = playerList[i].GetComponent<Inventory>();
            float rlifePoint = (float) Math.Round(_playerStatsManager.lifePoint * 100f) / 100f;
            float rmaxLifePoint = (float) Math.Round(_playerStatsManager.maxLifePoint * 100f) / 100f;
            float rUltPoint = (float) Math.Round(_playerStatsManager.actualUltPoint * 100f) / 100f;
            float rmaxUltPoint = (float) Math.Round(_playerStatsManager.ultMaxPoint * 100f) / 100f;

            if (i == 0)
            {
                p1_healthBarImg.fillAmount = rlifePoint / rmaxLifePoint;
                p1_energyBarImage.fillAmount = rUltPoint / rmaxUltPoint;
                p1_healthText.text = rlifePoint + "/" + rmaxLifePoint;
                p1_moneyText.text = currentMoney.ToString();
            }
            /*
            if (i == 1)
            {
                p2_healthBarImg.fillAmount = rlifePoint / rmaxLifePoint;
                p2_energyBarImage.fillAmount = rUltPoint / rmaxUltPoint;
                p2_healthText.text = rlifePoint + "/" + rmaxLifePoint;
                p2_moneyText.text = currentMoney.ToString();
            }
            */
        }
    }

    public void SetMasterVolume(float mstLvl)
    {
        masterMixer.SetFloat("masterVolume", mstLvl);
    }

    public void SetMusicVolume(float mscLvl)
    {
        masterMixer.SetFloat("musicVolume", mscLvl);
    }

    public void SetEffectVolume(float vlmLvl)
    {
        masterMixer.SetFloat("effectVolume", vlmLvl);
    }

    public void SetInterfaceVolume(float intLvl)
    {
        masterMixer.SetFloat("interfaceVolume", intLvl);
    }

}

