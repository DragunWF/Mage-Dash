using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Player : MonoBehaviour
{
    public float DamageCooldown { get; private set; }

    private bool isAlive = true;
    private int health;
    private int mana;

    private const float spellCooldownTime = 0.125f;
    private const float manaRegenTime = 3.45f; // change in the future
    private bool onSpellCooldown = false;
    private bool onDamageCooldown = false;

    private bool manaPowerupActive = false;
    private bool scorePowerupActive = false;
    private bool damagePowerupActive = false;

    private const float jumpForce = 13.5f;
    private bool onGround = true;
    private Rigidbody2D rigidBody;

    private GameStats gameStats;
    private GameUI gameUI;

    private GameObject fireball;
    private Transform firePos;

    private FadeToBlack screen;
    private FlashEffect flashEffect;
    private AudioPlayer audioPlayer;

    public void DamageHealth()
    {
        if (!onDamageCooldown)
        {
            health--;
            onDamageCooldown = true;
            gameUI.UpdateHealthBar(health);
            flashEffect.Flash();
            Invoke("RemoveInvincibility", DamageCooldown);

            if (health <= 0)
            {
                Death();
            }
            else
            {
                audioPlayer.PlayDamage();
            }
        }
    }

    public void ActivatePowerup(string powerupType, float powerupDuration)
    {
        audioPlayer.PlayPowerup();

        if (powerupType == "health")
        {
            health++;
        }
        else if (powerupType == "mana" && !manaPowerupActive)
        {
            manaPowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            Invoke("DisableManaPowerup", powerupDuration);
        }
        else if (powerupType == "score" && !scorePowerupActive)
        {
            manaPowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            Invoke("DisableManaPowerup", powerupDuration);
        }
        else if (powerupType == "damage" && !damagePowerupActive)
        {
            damagePowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            Invoke("DisableDamagePowerup", powerupDuration);
        }
    }

    #region Powerup Getter Methods

    public bool GetManaPowerupStatus() { return manaPowerupActive; }
    public bool GetScorePowerupStatus() { return scorePowerupActive; }
    public bool GetDamagePowerupStatus() { return damagePowerupActive; }

    #endregion

    private void Awake()
    {
        DamageCooldown = 2.25f;

        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        firePos = GameObject.Find("FirePos").transform;

        rigidBody = GetComponent<Rigidbody2D>();
        gameStats = FindObjectOfType<GameStats>();
        gameUI = FindObjectOfType<GameUI>();

        flashEffect = GetComponent<FlashEffect>();
        screen = FindObjectOfType<FadeToBlack>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnJump()
    {
        if (onGround)
        {
            onGround = false;
            rigidBody.velocity += new Vector2(0, jumpForce);
            audioPlayer.PlayJump();
        }
    }

    private void OnFire()
    {
        if (mana > 0 && !onSpellCooldown)
        {
            Instantiate(fireball, firePos.position, Quaternion.identity);
            mana--;
            gameUI.UpdateManaBar(mana);
            audioPlayer.PlayShoot();
            onSpellCooldown = true;
            Invoke("RestoreSpell", spellCooldownTime);
        }
    }

    private void Death()
    {
        isAlive = false;
        FindObjectOfType<ParticlePlayer>().PlayDeath(transform.position);
        FindObjectOfType<FadeToBlack>().InitializeFade();
        Destroy(gameObject);
    }

    #region Disable Methods

    private void DisableManaPowerup()
    {
        manaPowerupActive = false;
        gameUI.ModifyPowerups("mana", false);
    }

    private void DisableScorePowerup()
    {
        scorePowerupActive = false;
        gameUI.ModifyPowerups("score", false);
    }

    private void DisableDamagePowerup()
    {
        damagePowerupActive = false;
        gameUI.ModifyPowerups("damage", false);
    }

    private void RestoreSpell() { onSpellCooldown = false; }
    private void RemoveInvincibility() { onDamageCooldown = false; }

    #endregion

    private IEnumerator RegenMana()
    {
        const float interval = 1.5f;
        const float upgradedRegen = manaRegenTime * 0.15f;

        while (true)
        {
            if (mana < gameStats.MaxPlayerMana)
            {
                yield return new WaitForSeconds(manaPowerupActive ? manaRegenTime : upgradedRegen);
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
        const float baseScoreIncrease = 1.25f, triggerDelay = 3f;
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
