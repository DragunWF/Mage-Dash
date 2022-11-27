using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private GameObject playerDeathEffect;
    private GameObject obstacleDeathEffect;
    private GameObject fireballEffect;

    public void PlayDeath(Vector2 position)
    {
        SpawnParticle(playerDeathEffect, position);
    }

    public void PlayFireballHit(Vector2 position)
    {
        SpawnParticle(fireballEffect, position);
    }

    public void PlayObstacleDeath(Vector2 position)
    {
        SpawnParticle(obstacleDeathEffect, position);
    }

    private void Awake()
    {
        playerDeathEffect = Resources.Load("Prefabs/PlayerDeathEffect") as GameObject;
        obstacleDeathEffect = Resources.Load("Prefabs/ObstacleDeathEffect") as GameObject;
        fireballEffect = Resources.Load("Prefabs/FireballEffect") as GameObject;
    }

    private void SpawnParticle(GameObject effect, Vector2 position)
    {
        Instantiate(effect, position, Quaternion.identity);
    }
}
