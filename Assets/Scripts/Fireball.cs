using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Fireball : MonoBehaviour
{
    private const float xLimit = 8f;
    private const float speed = 6.5f;

    private void Update()
    {
        float deltaTime = speed * Time.deltaTime;
        transform.Translate(deltaTime, 0, 0);

        if (transform.position.x >= xLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            FindObjectOfType<ParticlePlayer>().PlayHitEffect(transform.position);
            Destroy(gameObject);
        }
    }
}
