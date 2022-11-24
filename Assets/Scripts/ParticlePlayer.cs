using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticlePlayer : MonoBehaviour
{
    private GameObject playerDeathEffect;

    public void PlayPlayerDeath(Vector2 pos)
    {
        Instantiate(playerDeathEffect, pos, Quaternion.identity);
    }

    private void Awake()
    {
        playerDeathEffect = Resources.Load("Prefabs/PlayerDeathEffect") as GameObject;
    }

    private void Update()
    {

    }
}
