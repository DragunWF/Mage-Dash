using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioPlayer : MonoBehaviour
{
    private Dictionary<string, AudioClip> clips;
    private Dictionary<string, float> volumes;

    #region Play Audio File Methods

    public void PlayShoot() { PlayClip("Shoot"); }
    public void PlayDeath() { PlayClip("Death"); }
    public void PlayClick() { PlayClip("Click"); }
    public void PlayJump() { PlayClip("Jump"); }
    public void PlayPickup() { PlayClip("Pickup"); }
    public void PlayUpgrade() { PlayClip("Upgrade"); }
    public void PlayError() { PlayClip("Error"); }
    public void PlayDamage() { PlayClip("Damage"); }

    #endregion

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

    private void PlayClip(string fileName)
    {
        if (clips[fileName] != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clips[fileName], cameraPos, volumes[fileName]);
        }
    }
}
