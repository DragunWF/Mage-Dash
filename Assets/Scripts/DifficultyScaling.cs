using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DifficultyScaling : MonoBehaviour
{
    public int DifficultyLevel { get; private set; }

    private Dictionary<int, float> timeIntervals = new Dictionary<int, float>();
    private const int maxDifficultyLevel = 10;
    private const float baseTimeInterval = 15f;

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

        CalculateScaling();
    }

    private void Start()
    {
        StartCoroutine(Scale());
    }

    private void CalculateScaling()
    {
        float modifier = 2.5f;

        for (int i = 1; i <= maxDifficultyLevel; i++)
        {
            timeIntervals.Add(i, baseTimeInterval + modifier * i);
            modifier += i >= 5 ? 2.5f : 0.5f;
        }
    }

    private IEnumerator Scale()
    {
        const float scaleDelay = 2.5f;
        yield return new WaitForSeconds(scaleDelay);

        while (true)
        {
            yield return new WaitForSeconds(timeIntervals[DifficultyLevel]);
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
