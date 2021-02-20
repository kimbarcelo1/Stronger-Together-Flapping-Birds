using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public GameObject creditsPanel;
    public GameObject[] menuObjects;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowCredits()
    {
        foreach (GameObject item in menuObjects)
        {
            item.SetActive(false);
        }

        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);

        foreach (GameObject item in menuObjects)
        {
            item.SetActive(true);
        }
    }
}
