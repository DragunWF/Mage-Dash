using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioPlayer : MonoBehaviour
{
    private Vector2 defaultPosition = new Vector2(0, 0);
    private AudioClip fireball;
    private AudioClip death;
    private AudioClip click; // add audio files

    public void PlayFireball()
    {
        PlayClip(fireball, defaultPosition);
    }

    public void PlayDeath()
    {
        PlayClip(death, defaultPosition);
    }

    public void PlayClick()
    {
        PlayClip(click, defaultPosition);
    }

    private void Awake()
    {
        fireball = Resources.Load("Audio/fireball") as AudioClip;
        death = Resources.Load("Audio/death") as AudioClip;
        click = Resources.Load("Audio/click") as AudioClip;
    }

    private void PlayClip(AudioClip clip, Vector2 position)
    {

    }
}
