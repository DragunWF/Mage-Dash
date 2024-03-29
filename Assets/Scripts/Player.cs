using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Player : MonoBehaviour
{
    public float DamageCooldown { get; private set; }
    public int SpellDamage { get; private set; }

    private bool isAlive = true;
    private int health;
    private int mana;

    private const float spellCooldownTime = 0.125f;
    private bool onSpellCooldown = false;
    private bool onDamageCooldown = false;
    private float manaRegenTime;

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

    #region Powerup Getter Methods

    public bool GetManaPowerupStatus() => manaPowerupActive;
    public bool GetScorePowerupStatus() => scorePowerupActive;
    public bool GetDamagePowerupStatus() => damagePowerupActive;

    #endregion

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

        if (powerupType == "health" && health < gameStats.MaxPlayerHealth)
        {
            health++;
            gameUI.UpdateHealthBar(health);
        }
        else if (powerupType == "mana" && !manaPowerupActive)
        {
            manaPowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            gameUI.UpdateManaBar(gameStats.MaxPlayerMana * 2, true);
            Invoke("DisableManaPowerup", powerupDuration);
        }
        else if (powerupType == "score" && !scorePowerupActive)
        {
            manaPowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            Invoke("DisableScorePowerup", powerupDuration);
        }
        else if (powerupType == "damage" && !damagePowerupActive)
        {
            damagePowerupActive = true;
            gameUI.ModifyPowerups(powerupType, true);
            Invoke("DisableDamagePowerup", powerupDuration);
        }
    }

    private void Awake()
    {
        DamageCooldown = 2.25f;

        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        firePos = GameObject.Find("FirePos").transform;

        rigidBody = GetComponent<Rigidbody2D>();
        gameUI = FindObjectOfType<GameUI>();

        flashEffect = GetComponent<FlashEffect>();
        screen = FindObjectOfType<FadeToBlack>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = FindObjectOfType<CosmeticManager>().EquippedCosmetic;
        gameStats = FindObjectOfType<GameStats>();

        gameStats.MaxPlayerHealth = gameStats.ComputeHealth();
        gameStats.MaxPlayerMana = gameStats.ComputeManaCapacity();

        health = gameStats.MaxPlayerHealth;
        mana = gameStats.MaxPlayerMana;
        manaRegenTime = gameStats.ComputeManaRegen();
        SpellDamage = gameStats.ComputeDamage();

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
        else
        {
            audioPlayer.PlayError();
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
        if (mana > gameStats.MaxPlayerMana)
        {
            mana = gameStats.MaxPlayerMana;
        }
        gameUI.UpdateManaBar(gameStats.MaxPlayerMana, true);

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

    #endregion

    #region Misc Boolean Setters

    private void RestoreSpell() => onSpellCooldown = false;
    private void RemoveInvincibility() => onDamageCooldown = false;

    #endregion

    private IEnumerator RegenMana()
    {
        const float interval = 1.5f;
        float upgradedRegen = manaRegenTime * 0.15f;

        while (true)
        {
            if (mana < (manaPowerupActive ? gameStats.MaxPlayerMana * 2 : gameStats.MaxPlayerMana))
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

        float scoreInterval = 1.5f * gameStats.ScoreModifier; // 3 is the base interval
        while (isAlive)
        {
            yield return new WaitForSeconds(scoreInterval);
            gameStats.IncreaseScore(baseScoreIncrease);
        }
    }
}
