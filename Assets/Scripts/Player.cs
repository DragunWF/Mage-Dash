using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float DamageCooldown { get; private set; }

    private bool isAlive = true;
    private int health;
    private int mana;

    private const float spellCooldownTime = 0.25f;
    private const float manaRegenTime = 3.5f; // change in the future
    private bool onSpellCooldown = false;
    private bool onDamageCooldown = false;

    private const float jumpForce = 12.5f;
    private Rigidbody2D rigidBody;

    private GameStats gameStats;
    private GameUI gameUI;

    private GameObject fireball;
    private Transform firePos;
    private FlashEffect flashEffect;

    public void DamageHealth()
    {
        if (!onDamageCooldown)
        {
            health--;
            onDamageCooldown = true;
            gameUI.UpdateHealthBar(health);
            flashEffect.Flash();
            Invoke("RemoveInvincibility", DamageCooldown);
        }
    }

    private void Awake()
    {
        DamageCooldown = 4.25f;

        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        firePos = GameObject.Find("FirePos").transform;
        flashEffect = GetComponent<FlashEffect>();

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
        StartCoroutine(RegenMana());
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
        if (mana > 0 && !onSpellCooldown)
        {
            Instantiate(fireball, firePos.position, Quaternion.identity);
            mana--;
            gameUI.UpdateManaBar(mana);
            onSpellCooldown = true;
            Invoke("RestoreSpell", spellCooldownTime);
        }
    }

    private void RestoreSpell()
    {
        onSpellCooldown = false;
    }

    private void RemoveInvincibility()
    {
        onDamageCooldown = false;
    }

    private IEnumerator RegenMana()
    {
        const float interval = 1.5f;

        while (true)
        {
            if (mana < gameStats.MaxPlayerMana)
            {
                yield return new WaitForSeconds(manaRegenTime);
                mana++;
                gameUI.UpdateManaBar(mana);
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }
        }
    }

    private IEnumerator OnAlive()
    {
        const float baseScoreIncrease = 1.25f;
        const float triggerDelay = 3f;
        yield return new WaitForSeconds(triggerDelay);

        float scoreInterval = 1.5f * gameStats.ScoreModifier / 2; // 3 is the base interval
        while (isAlive)
        {
            yield return new WaitForSeconds(scoreInterval);
            gameStats.IncreaseScore(baseScoreIncrease);
            yield return new WaitForSeconds(scoreInterval);
        }
    }
}
