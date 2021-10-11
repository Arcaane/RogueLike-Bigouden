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

    // Update is called once per frame
    private void Update()
    {
    }

    public void StartGame()
    {
        HideMenu();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Hub"));
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
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