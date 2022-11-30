using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private GameObject deathEffect;
    private GameObject hitEffect;

    public void PlayDeath(Vector2 position)
    {
        SpawnParticle(deathEffect, position);
    }

    public void PlayHitEffect(Vector2 position)
    {
        SpawnParticle(hitEffect, position);
    }

    private void Awake()
    {
        deathEffect = Resources.Load("Prefabs/DeathEffect") as GameObject;
        hitEffect = Resources.Load("Prefabs/HitEffect") as GameObject;
    }

    private void SpawnParticle(GameObject effect, Vector2 position)
    {
        Instantiate(effect, position, Quaternion.identity);
    }
}
