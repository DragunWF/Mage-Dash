using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Powerup : MonoBehaviour
{
    [Header("Powerup Attributes")]
    [SerializeField] string powerupType;

    private const float despawnPointX = -8f;
    private const float baseSpeed = 2.5f;
    private float speed;

    private DifficultyScaling difficulty;
    private AudioPlayer audioPlayer;

    public string GetPowerupType()
    {
        return powerupType; // use for player collection (future use)
    }

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        difficulty = FindObjectOfType<DifficultyScaling>();
        UpdateSpeed();
    }

    private void Update()
    {
        float deltaSpeed = speed * Time.deltaTime;
        transform.Translate(deltaSpeed, 0, 0);

        if (transform.position.x <= despawnPointX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // add sound effect
            Destroy(gameObject);
        }
    }

    private void UpdateSpeed()
    {
        int level = difficulty.DifficultyLevel;
        speed = baseSpeed;

        if (level > 1)
        {
            const float incrementor = 0.125f;
            speed += level * incrementor;
        }
    }
}
