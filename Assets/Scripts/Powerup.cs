using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Powerup : MonoBehaviour
{
    [Header("Powerup Attributes")]
    [SerializeField] string powerupType;
    [SerializeField] float duration;

    private const float despawnPointX = -8f;
    private const float baseSpeed = 2.5f;
    private float speed;
    private DifficultyScaling difficulty;

    public string GetPotionType()
    {
        return powerupType;
    }

    public float GetDuration()
    {
        return duration;
    }

    private void Awake()
    {
        difficulty = FindObjectOfType<DifficultyScaling>();
        UpdateSpeed();
    }

    private void Update()
    {
        float deltaSpeed = speed * Time.deltaTime;
        transform.Translate(-deltaSpeed, 0, 0);

        if (transform.position.x <= despawnPointX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioPlayer>().PlayPickup();
            FindObjectOfType<ParticlePlayer>().PlayPickup(transform.position);
            FindObjectOfType<Player>().ActivatePowerup(powerupType, duration);
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
            speed += (level - 1) * incrementor;
        }
    }
}
