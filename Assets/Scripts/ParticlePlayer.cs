using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();
    private const float destroyDelay = 0.5f;

    #region Play Particle Methods

    public void PlayDeath(Vector2 position)
    {
        SpawnParticle("Death", position);
    }

    public void PlayHit(Vector2 position)
    {
        SpawnParticle("Hit", position);
    }

    public void PlayPickup(Vector2 position)
    {
        SpawnParticle("Pickup", position);
    }

    #endregion

    private void Awake()
    {
        particles.Add("Death", Resources.Load("Prefabs/DeathEffect") as GameObject);
        particles.Add("Pickup", Resources.Load("Prefabs/PickupEffect") as GameObject);
        particles.Add("Hit", Resources.Load("Prefabs/HitEffect") as GameObject);
    }

    private void SpawnParticle(string effectName, Vector2 position)
    {
        GameObject particle = Instantiate(particles[effectName], position,
                                          Quaternion.identity);
        Destroy(particle, destroyDelay);
    }
}
