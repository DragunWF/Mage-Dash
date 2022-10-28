using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float Speed { get; private set; }

    private float initialSpeedValue = 1.5f;
    private float xResetPoint = 14;

    public void UpdateSpeed(float newValue)
    {
        Speed = newValue;
    }

    private void Awake()
    {
        Speed = initialSpeedValue;
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
