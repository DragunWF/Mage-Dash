using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioPlayer : MonoBehaviour
{
    private Dictionary<string, AudioClip> clips;
    private Dictionary<string, float> volumes;

    public void PlayShoot()
    {
        const string fileName = "Shoot";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayDeath()
    {
        const string fileName = "Death";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayClick()
    {
        const string fileName = "Click";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayJump()
    {
        const string fileName = "Jump";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayPickup()
    {
        const string fileName = "Pickup";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayUpgrade()
    {
        const string fileName = "Upgrade";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayError()
    {
        const string fileName = "Error";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    public void PlayDamage()
    {
        const string fileName = "Damage";
        PlayClip(clips[fileName], volumes[fileName]);
    }

    private void Awake()
    {
        clips.Add("Shoot", Resources.Load("Audio/Shoot") as AudioClip);
        volumes.Add("Shoot", 0.5f);

        clips.Add("Death", Resources.Load("Audio/Shoot") as AudioClip);
        volumes.Add("Death", 0.75f);

        clips.Add("Click", Resources.Load("Audio/Click") as AudioClip);
        volumes.Add("Click", 0.85f);

        clips.Add("Jump", Resources.Load("Audio/Jump") as AudioClip);
        volumes.Add("Jump", 0.35f);

        clips.Add("Upgrade", Resources.Load("Audio/Upgrade") as AudioClip);
        volumes.Add("Upgrade", 0.85f);

        clips.Add("Pickup", Resources.Load("Audio/Pickup") as AudioClip);
        volumes.Add("Pickup", 0.75f);

        clips.Add("Error", Resources.Load("Audio/Error") as AudioClip);
        volumes.Add("Error", 0.85f);

        clips.Add("Damage", Resources.Load("Audio/Damage") as AudioClip);
        volumes.Add("Damage", 0.55f);
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
