using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private const int mainMenuSceneIndex = 0;
    private const int gameSceneIndex = 1;
    private const int retryMenuSceneIndex = 2;
    private const int shopMenuSceneIndex = 3;

    public void LoadGameScene()
    {
        LoadScene(gameSceneIndex);
    }

    public void LoadRetryMenu()
    {
        FindObjectOfType<GameStats>().SaveScore();
        LoadScene(retryMenuSceneIndex);
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuSceneIndex);
    }

    public void LoadShopMenu()
    {
        LoadScene(shopMenuSceneIndex);
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == gameSceneIndex)
        {
            FindObjectOfType<GameStats>().OnGameReset();
        }
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
