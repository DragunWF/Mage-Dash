using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float Speed { get; private set; }

    private float initialSpeedValue = 0.5f;
    private float xResetPoint = 14;

    public void UpdateSpeed(float newValue)
    {
        Speed = -newValue * Time.deltaTime;
    }

    private void Awake()
    {
        Speed = -initialSpeedValue * Time.deltaTime;
    }

    private void Update()
    {
        if (transform.position.x < -xResetPoint)
            transform.position = new Vector2(xResetPoint, 0);
        else
            transform.Translate(Speed, 0, 0);
    }
}
