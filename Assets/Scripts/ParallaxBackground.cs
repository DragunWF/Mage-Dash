using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParallaxBackground : MonoBehaviour
{
    public float Speed { get; private set; }
    private const float initialSpeedValue = 1.5f;
    private const float xResetPoint = 14;
    private DifficultyScaling difficulty;

    public void UpdateSpeed()
    {
        Speed = initialSpeedValue + ((difficulty.DifficultyLevel - 1) * 0.40f);
    }

    private void Awake()
    {
        Speed = initialSpeedValue;
        difficulty = FindObjectOfType<DifficultyScaling>();
    }

    private void Update()
    {
        if (transform.position.x < -xResetPoint)
        {
            transform.position = new Vector2(xResetPoint, 0);
        }
        else
        {
            float deltaSpeed = Speed * Time.deltaTime;
            transform.Translate(-deltaSpeed, 0, 0);
        }
    }
}
