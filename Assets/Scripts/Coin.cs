using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coin : MonoBehaviour
{
    private const float baseSpeed = 1.5f;
    private const float despawnPositionX = -8f;
    private float modifiedSpeed;

    private void Awake()
    {
        int level = FindObjectOfType<DifficultyScaling>().DifficultyLevel;
        modifiedSpeed = baseSpeed + 0.05f * level;
    }

    private void Update()
    {
        float deltaSpeed = modifiedSpeed * Time.deltaTime;
        transform.Translate(-deltaSpeed, 0, 0);

        if (transform.position.x <= despawnPositionX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameStats>().CollectCoin();
            FindObjectOfType<AudioPlayer>().PlayPickup();
            Destroy(gameObject);
        }
    }
}
