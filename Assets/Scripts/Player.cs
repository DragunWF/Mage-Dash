using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool isAlive = true;
    private int health;
    private int mana;

    private const float jumpForce = 12.5f;
    private Rigidbody2D rigidBody;

    private GameStats gameStats;
    private GameUI gameUI;

    public void DamageHealth()
    {
        health -= 1;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameStats = FindObjectOfType<GameStats>();
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Start()
    {
        health = gameStats.MaxPlayerHealth;
        mana = gameStats.MaxPlayerMana;

        gameUI.UpdateHealthBar(health, true);
        gameUI.UpdateManaBar(mana, true);

        StartCoroutine(OnAlive());
    }

    private void Update()
    {

    }

    private void OnJump()
    {
        rigidBody.velocity += new Vector2(0, jumpForce);
        // Add sound effect in the future
    }

    private void OnFire()
    {

    }

    private IEnumerator OnAlive()
    {
        const float triggerDelay = 3f;
        yield return new WaitForSeconds(triggerDelay);

        float scoreInterval = 3f * gameStats.ScoreModifier / 2; // 3 is the base interval
        while (isAlive)
        {
            yield return new WaitForSeconds(scoreInterval);
            gameStats.IncreaseScore(1);
            yield return new WaitForSeconds(scoreInterval);
        }
    }
}
