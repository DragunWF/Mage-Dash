using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Cactus : MonoBehaviour
{
    private const float despawnPositionX = -8.5f;
    private ParallaxBackground background;

    private void Awake()
    {
        background = FindObjectOfType<ParallaxBackground>();
    }

    private void Update()
    {
        float deltaSpeed = background.Speed * Time.deltaTime;
        transform.Translate(-deltaSpeed, 0, 0);

        if (transform.position.x <= despawnPositionX)
        {
            Destroy(gameObject);
        }
    }
}
