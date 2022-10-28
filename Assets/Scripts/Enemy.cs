using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Obstacle Stats")]
    [SerializeField] float health = 25f;
    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 5f;

    [Header("Obstacle Type")]
    [SerializeField] bool isFlying = false;

    private Rigidbody2D rigidBody;
    private const float despawnPositionX = -8f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.localPosition = new Vector2(0, 0);
    }

    private void Update()
    {
        Move();
        CheckDespawnPoint();
    }

    private void Move()
    {
        float deltaSpeed = -speed * Time.deltaTime;
        if (isFlying)
        {
            transform.Translate(deltaSpeed, 0, 0);
        }
        else
        {
            const float groundYpos = -3f;
            transform.Translate(deltaSpeed, 0, 0);
            if (transform.position.y <= groundYpos)
            {
                transform.position = new Vector3(transform.position.x, -2f);
            }
        }
    }

    private void CheckDespawnPoint()
    {
        if (transform.position.x <= despawnPositionX)
        {
            Destroy(gameObject);
        }
    }

    private void DamageHealth()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                FindObjectOfType<Player>().DamageHealth();
                break;
            case "Fireball":
                DamageHealth();
                break;
        }
    }
}
