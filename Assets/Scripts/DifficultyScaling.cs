using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaling : MonoBehaviour
{
    public int DifficultyLevel { get; private set; }
    private const int maxDifficultyLevel = 10;
    private const float timeToScaleDifficulty = 15f;

    private void Awake()
    {
        DifficultyLevel = 1;
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
            if (DifficultyLevel >= maxDifficultyLevel)
            {
                break;
            }
        }
    }
}
