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

    private void Awake()
    {
        shoot = Resources.Load("Audio/Shoot") as AudioClip;
        death = Resources.Load("Audio/Death") as AudioClip;
        click = Resources.Load("Audio/Click") as AudioClip;
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
