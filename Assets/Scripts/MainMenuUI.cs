using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainMenuUI : MonoBehaviour
{
    private bool toggleTutorialUI = false;
    private GameObject mainMenuUI;
    private GameObject tutorialUI;
    private GameObject[] menuBackground;

    public void ToggleTutorialUI()
    {
        toggleTutorialUI = toggleTutorialUI ? false : true;
        bool toggleMainMenuUI = toggleTutorialUI ? false : true;

        tutorialUI.SetActive(toggleTutorialUI);
        mainMenuUI.SetActive(toggleMainMenuUI);
        
        foreach (GameObject instance in menuBackground)
        {
            instance.SetActive(toggleMainMenuUI);
        }
        if (toggleTutorialUI)
        {
            DeleteBackgroundEntities();
        }
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

    private void DeleteBackgroundEntities()
    {
        Cactus[] props = FindObjectsOfType<Cactus>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Cactus cactus in props)
        {
            Destroy(cactus.gameObject);
        }

        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
