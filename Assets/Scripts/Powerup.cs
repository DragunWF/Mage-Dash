using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Powerup : MonoBehaviour
{
    [Header("Powerup Attributes")]
    [SerializeField] string powerupType;

    private const float speed = 2.5f;
    private const float despawnPointX = -8f;
    private AudioPlayer audioPlayer;

    public string GetPowerupType()
    {
        return powerupType; // use for player collection (future use)
    }

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // add sound effect
            Destroy(gameObject);
        }
    }
}