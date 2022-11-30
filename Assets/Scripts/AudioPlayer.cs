using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioPlayer : MonoBehaviour
{
    private AudioClip shoot;
    private const float shootVolume = 0.5f;

    private AudioClip death;
    private const float deathVolume = 0.75f;

    private AudioClip click;
    private const float clickVolume = 0.65f;

    private AudioClip jump;
    private const float jumpVolume = 0.35f;

    private AudioClip upgrade;
    private const float upgradeVolume = 0.85f;

    private AudioClip error;
    private const float errorVolume = 0.85f;

    private AudioClip damage;
    private const float damageVolume = 0.55f;

    public void PlayShoot()
    {
        PlayClip(shoot, shootVolume);
    }

    public void PlayDeath()
    {
        PlayClip(death, deathVolume);
    }

    public void PlayClick()
    {
        PlayClip(click, clickVolume);
    }

    public void PlayJump()
    {
        PlayClip(jump, jumpVolume);
    }

    public void PlayUpgrade()
    {
        PlayClip(upgrade, upgradeVolume);
    }

    public void PlayError()
    {
        PlayClip(error, errorVolume);
    }

    public void PlayDamage()
    {
        PlayClip(damage, damageVolume);
    }

    private void Awake()
    {
        shoot = Resources.Load("Audio/Shoot") as AudioClip;
        death = Resources.Load("Audio/Death") as AudioClip;
        click = Resources.Load("Audio/Click") as AudioClip;
        jump = Resources.Load("Audio/Jump") as AudioClip;
        upgrade = Resources.Load("Audio/Upgrade") as AudioClip;
        error = Resources.Load("Audio/Error") as AudioClip;
        damage = Resources.Load("Audio/Damage") as AudioClip;
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
