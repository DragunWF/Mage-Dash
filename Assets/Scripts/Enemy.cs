using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    [Header("Obstacle Stats")]
    [SerializeField] float health = 25f;
    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 5f;

    [Header("Obstacle Type")]
    [SerializeField] bool isFlying = false;

    [Header("Misc Stats")]
    [SerializeField] float scoreGain = 25f;

    public float DamageCooldown { get; private set; }

    private FlashEffect flashEffect;
    private Rigidbody2D rigidBody;
    private const float despawnPositionX = -8f;

    private GameStats gameStats;
    private DifficultyScaling difficulty;
    private AudioPlayer audioPlayer;
    private Player player;

    private void Awake()
    {
        DamageCooldown = 0.25f;

        flashEffect = GetComponent<FlashEffect>();
        rigidBody = GetComponent<Rigidbody2D>();

        difficulty = FindObjectOfType<DifficultyScaling>();
        gameStats = FindObjectOfType<GameStats>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        transform.localPosition = new Vector2(0, 0);

        if (difficulty != null && difficulty.DifficultyLevel > 1)
        {
            for (int i = 1; i < difficulty.DifficultyLevel; i++)
            {
                speed += 0.35f;
                damage += 2.5f;
                health += 4.5f;
                scoreGain += 7.5f;
            }
        }
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
        flashEffect.Flash();
        health -= player.GetDamagePowerupStatus() ?
            player.SpellDamage * 2 : player.SpellDamage;
        audioPlayer.PlayDamage();

        if (health <= 0)
        {
            bool activeDoubleScore = player.GetScorePowerupStatus();
            gameStats.IncreaseScore(activeDoubleScore ? scoreGain * 2 : scoreGain);
            FindObjectOfType<ParticlePlayer>().PlayDeath(transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                player.DamageHealth();
                break;
            case "Fireball":
                DamageHealth();
                break;
        }
    }
}
