using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DifficultyScaling : MonoBehaviour
{
    public int DifficultyLevel { get; private set; }

    private const int maxDifficultyLevel = 10;
    private const float timeToScaleDifficulty = 15f;

    private GameUI gameUI;
    private ObstacleSpawner spawner;
    private ParallaxBackground[] backgrounds;

    public int GetMaxDifficultyLevel() { return maxDifficultyLevel; }

    private void Awake()
    {
        DifficultyLevel = 1;
        gameUI = FindObjectOfType<GameUI>();
        spawner = FindObjectOfType<ObstacleSpawner>();
        backgrounds = FindObjectsOfType<ParallaxBackground>();
    }

    private void Start()
    {
        StartCoroutine(Scale());
    }

    private IEnumerator Scale()
    {
        const float scaleDelay = 2.5f;
        yield return new WaitForSeconds(scaleDelay);

        while (true)
        {
            yield return new WaitForSeconds(timeToScaleDifficulty);
            DifficultyLevel++;
            gameUI.UpdateDifficulty(DifficultyLevel);
            spawner.ScaleSpawner();

            foreach (ParallaxBackground instance in backgrounds)
            {
                instance.UpdateSpeed();
            }

            if (DifficultyLevel >= maxDifficultyLevel)
            {
                break;
            }
        }
    }
}
