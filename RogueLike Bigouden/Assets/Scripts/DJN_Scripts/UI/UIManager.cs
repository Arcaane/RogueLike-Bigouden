using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject player;
    private PlayerInput_Final _playerInputFinal;

    private Inventory inventory;
    public Transform[] imageItemPanel;
    public Transform itemPanelParent;

    [Header("DialogueBox")] public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI immersiveDialogue;

    [Header("Common UI")] 
    public GameObject itemInformationPanel;

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemPriceText;
    public Image itemIconImage;
    public TextMeshProUGUI itemRarityText;

    [Header("Player1 HUD")] public GameObject player1UI;
    public Image p1_healthBarImg;
    public Image p1_backgroundBarImg;
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

    [SerializeField] private GameObject pauseMenuPanel;
    public bool isPaused;
    [SerializeField] private GameObject blur;

    [Header("Pause Menu Score")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI killNumber;
    public TextMeshProUGUI moneyCollect;
    
    [Header("Settings")] 
    public GameObject settingPanel;

    public AudioMixer masterMixer;

    [Header("Test Information")] [Range(0, 10)]
    public float currentHealth;

    public float maxHealth;
    [Range(0, 999)] public int currentMoney;
    [Range(0, 100)] public float currentEnergy;
    public float maxEnergy;

    public EventSystem eventSystem;
    public GameObject pauseFirstSelectedButton;

    public bool searchInventory;
    private PlayerStatsManager _playerStatsManager;
    public List<GameObject> playerList;

    public GameObject actualPanel;
    
    
    [HideInInspector] public bool open;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Animator goBackground;
    public Animator goForeground;

    public TextMeshProUGUI enemyKilledText;
    public TextMeshProUGUI moneyCollectText;
    public TextMeshProUGUI timerFinalText;
    public TextMeshProUGUI scoreText;
    
    [Header("Player HUD Animations")]
    public Animator itemAnimation;

    public Animator playerAnimation;
    public Animator moneyAnimation;
    public bool earnMoney;
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        searchInventory = false;
        dialogueBox.SetActive(false);
        isPaused = false;
        pauseMenu.SetActive(false);
        eventSystem = FindObjectOfType<EventSystem>();
        itemInformationPanel.SetActive(false);
    }

    private void Update()
    {
        UpdateItemPlayer();
        
           

        if (earnMoney)
        {
            moneyAnimation.SetBool("addMoney", true);
            earnMoney = false;
        }
        else
        {
            moneyAnimation.SetBool("addMoney", false);
        }
    }

    #region SETUP

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

        _playerInputFinal = player.GetComponent<PlayerInput_Final>();

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
                p1_energyBarImage.fillAmount = rUltPoint / rmaxUltPoint;
                
                StartCoroutine(RefreshHealthBar(rlifePoint, rmaxLifePoint));
                p1_moneyText.text = currentMoney.ToString();
                p1_moneyText.text = PlayerStatsManager.playerStatsInstance.money.ToString();
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

        IEnumerator RefreshHealthBar(float lifePoint, float maxLifePoint)
        {
            
            p1_healthBarImg.fillAmount = lifePoint / maxLifePoint;
            yield return new WaitForSeconds(1);
            p1_backgroundBarImg.fillAmount = lifePoint / maxLifePoint;

        }
        
    }

    #endregion

    #region MAIN MENU

    //LOOK AT INTERFACE SCRIPT

    #endregion

    #region GAMEOVER

    public void LoadGameOver()
    {
        gameOverPanel.SetActive(true);
        isPaused = true;
        goBackground.Play("background_appear");
        goForeground.Play("foreground_appear");

        enemyKilledText.text = ScoreManager.instance.enemyKilled.ToString();
        moneyCollectText.text = ScoreManager.instance.moneyObtained.ToString();
        timerFinalText.text = $"{ScoreManager.instance.minutes:00}:{ScoreManager.instance.seconds:00}";
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BAV_HUB_BED_RESET");
        gameOverPanel.SetActive(false);
        PlayerStatsManager.playerStatsInstance.ResetPlayerStats();
        LoadManager.LoadManagerInstance.ResetProcedural();
        RefreshUI();
    }

    #endregion
    
    
    #region PAUSE
    public void Pause()
    {
        if (!isPaused)
        {
            SetSelectedButton(pauseFirstSelectedButton);
            isPaused = true;
            pauseMenu.SetActive(true);
            SoundManager.instance.PlaySound("menu_open");
            Time.timeScale = 0;
            
            SetPanel(pauseMenuPanel);
            
            
            if (blur)
            {
                blur.SetActive(true);
            }
        }

    }

    public void SetPanel(GameObject gameObject)
    {
        actualPanel = gameObject;
        actualPanel.SetActive(true);
    }

    public void SetSelectedButton(GameObject buttonSelected)
    {
        eventSystem.SetSelectedGameObject(buttonSelected);
    }

    public void ClosePanel()
    {
        actualPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        SoundManager.instance.PlaySound("click_back");
        
        if (actualPanel == pauseMenuPanel)
        {
            Resume();
        }
        else
        {
            actualPanel = pauseMenuPanel;
            SetSelectedButton(pauseFirstSelectedButton);
            SetPanel(pauseMenuPanel);
        }

    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            SoundManager.instance.PlaySound("menu_close");
            Time.timeScale = 1;
            SetSelectedButton(null);
            SetPanel(null);

            if (blur)
            {
                blur.SetActive(false);
            }
        }
    }

    public void BackToHome()
    {
        
        SceneManager.LoadScene(0);
        Debug.Log("Go To Hub");
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
    #endregion
   

    #region ITEMS

    public void OpenItemPanel()
    {
        //Load Animation OPENING
        itemAnimation.SetBool("open", open);
        //
        
    }
    
    void UpdateItemPlayer()
    {
        //ecart de 55 sur l'axe x
        imageItemPanel = itemPanelParent.GetComponentsInChildren<Transform>();

        if (inventory)
        {
          
                for (int i = 1; i < imageItemPanel.Length; i++)
                {
                    if (imageItemPanel.Length > inventory.items.Count)
                        imageItemPanel[i].GetComponent<Image>().enabled = false;
                }

                for (int i = 0; i < inventory.items.Count; i++)
                {
                    if (open)
                    {
                        imageItemPanel[i + 1].GetComponent<Image>().enabled = true;
                        imageItemPanel[i + 1].GetComponent<Image>().sprite = inventory.items[i].image;
                    }
                    else
                    {
                        imageItemPanel[i + 1].GetComponent<Image>().enabled = false;
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
    

    #endregion

    
    #region SOUND

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

    public void MuteSound(bool isMute)
    {
        if (isMute)
        {
            masterMixer.SetFloat("masterVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("masterVolume", 0);
        }
    }

    public void LoadSound(string soundName)
    {
        SoundManager.instance.PlaySound(soundName);
    }

    #endregion
    
}

