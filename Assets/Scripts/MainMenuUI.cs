using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainMenuUI : MonoBehaviour
{
    private GameObject mainMenuUI;
    private GameObject tutorialUI;
    private GameObject[] menuBackground;

    public void OpenTutorial()
    {
        mainMenuUI.SetActive(false);
        tutorialUI.SetActive(true);
    }

    public void CloseTutorial()
    {

    }

    private void Awake()
    {
        tutorialUI = GameObject.Find("HowToPlayUI");
        tutorialUI.SetActive(false);

        mainMenuUI = GameObject.Find("MainMenuUI");
        mainMenuUI.SetActive(true);

        menuBackground = new GameObject[3] {
            GameObject.Find("World") as GameObject,
            GameObject.Find("ObstacleSpawner") as GameObject,
            GameObject.Find("PropSpawner") as GameObject,
        };
    }
}
