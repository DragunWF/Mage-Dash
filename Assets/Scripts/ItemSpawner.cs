using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ItemSpawner : MonoBehaviour
{
    private float[] spawnIntervals = { 8.5f, 9f, 9.5f, 10f, 10.5f, 11f };
    private GameObject[] powerups;
    private Vector2[] spawnPositions;

    private void Awake()
    {
        powerups = new GameObject[3];
    }

    private void Start()
    {
        StartCoroutine(SpawnPowerups());
    }

    private int GetRandomIndex(int length)
    {
        return Random.Range(0, length);
    }

    private float GetRandomInterval()
    {
        int randomIndex = GetRandomIndex(spawnIntervals.Length);
        return spawnIntervals[randomIndex];
    }

    private GameObject GetRandomPowerup()
    {
        int randomIndex = GetRandomIndex(powerups.Length);
        return powerups[randomIndex];
    }

    private Vector2 GetRandomPosition()
    {
        int randomIndex = GetRandomIndex(spawnPositions.Length);
        return spawnPositions[randomIndex];
    }

    private IEnumerator SpawnPowerups()
    {
        const float spawnDelayTime = 2.5f;
        yield return new WaitForSeconds(spawnDelayTime);

        while (true)
        {
            yield return new WaitForSeconds(GetRandomInterval());

            GameObject powerup = GetRandomPowerup();
            Vector2 spawnPosition = GetRandomPosition();
            Instantiate(powerup, spawnPosition, Quaternion.identity);
        }
    }
}
