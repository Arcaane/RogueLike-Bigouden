using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndCredits : MonoBehaviour
{
    public TextMeshProUGUI ennemy;
    public TextMeshProUGUI money;
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;

    public GameObject buttonSelected;
    // Start is called before the first frame update
    public void Restart()
    {
        SceneManager.LoadScene("Splashscreen");
        SoundManager.instance.ResetSound();
        Destroy(FindObjectOfType<test>().gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        FindObjectOfType<UIManager>().player1UI.SetActive(false);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(buttonSelected);
        ScoreManager.instance.timerStart = false;
        ScoreManager.instance.DisplayTime();
        ScoreManager.instance.UpdateScore();
    }

    private void Start()
    {
        ennemy.text = ScoreManager.instance.enemyKilled.ToString();
        money.text = ScoreManager.instance.moneyObtained.ToString();
        score.text = ScoreManager.instance.score.ToString();
        time.text = $"{ScoreManager.instance.minutes:00}:{ScoreManager.instance.seconds:00}";
    }
}
