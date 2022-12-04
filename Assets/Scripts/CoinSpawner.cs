using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CoinSpawner : MonoBehaviour
{
    private const float baseSpawnInterval = 17.5f;
    private float modifiedInterval;

    private GameObject coin;
    private Vector2[] spawnPositions;
    private DifficultyScaling difficulty;

    public void UpdateSpawnSpeed()
    {
        float decreaseAmount = (difficulty.DifficultyLevel - 1) * 1.35f;
        modifiedInterval = baseSpawnInterval - decreaseAmount;
    }

    private void Awake()
    {
        coin = Resources.Load("Prefabs/Coin") as GameObject;
        spawnPositions = new Vector2[2] {
            GameObject.Find("Item Ground Point").transform.position,
            GameObject.Find("Item Upper Point").transform.position,
        };
        difficulty = FindObjectOfType<DifficultyScaling>();
    }

    private void Start()
    {
        modifiedInterval = baseSpawnInterval;
        StartCoroutine(SpawnCoins());
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)];
    }

    private IEnumerator SpawnCoins()
    {
        const float spawnDelay = 1.5f;
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return new WaitForSeconds(modifiedInterval);
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(coin, spawnPosition, Quaternion.identity);
        }
    }
}
