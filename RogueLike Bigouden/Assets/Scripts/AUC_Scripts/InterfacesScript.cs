using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfacesScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;

    private readonly List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    private void Start()
    {
        menu.SetActive(true);
        loadingInterface.SetActive(false);
    }
    
    public void StartGame()
    {
        //HideMenu();
        UIManager.instance.gameOverPanel.SetActive(false);
        scenesToLoad.Add(SceneManager.LoadSceneAsync("BAV_HUB_BED"));
    }

    public void StartCredits()
    {
        UIManager.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Credits");

    }

    public void StartMenu()
    {
        UIManager.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    
    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        UIManager.instance.gameOverPanel.SetActive(false);
        Application.Quit();
    }
    
    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    private IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (var i = 0; i < scenesToLoad.Count; i++)
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
    }
}