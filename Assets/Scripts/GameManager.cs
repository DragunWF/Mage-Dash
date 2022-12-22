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
    private const int devMenuSceneIndex = 4;
    private const int leaderboardSceneIndex = 5;

    #region Load Scene Methods

    public void LoadGameScene() => LoadScene(gameSceneIndex);
    public void LoadRetryMenu() => LoadScene(retryMenuSceneIndex);
    public void LoadMainMenu() => LoadScene(mainMenuSceneIndex);
    public void LoadShopMenu() => LoadScene(shopMenuSceneIndex);
    public void LoadDevMenu() => LoadScene(devMenuSceneIndex);
    public void LoadLeaderboard() => LoadScene(leaderboardSceneIndex);

    #endregion

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
        if (sceneIndex == retryMenuSceneIndex)
        {
            FindObjectOfType<GameStats>().SaveScore();
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
