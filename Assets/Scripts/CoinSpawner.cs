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

    private void Awake()
    {
        coin = Resources.Load("Prefabs/Coin") as GameObject;
        spawnPositions = new Vector2[2] {
            GameObject.Find("Item Point 1").transform.position,
            GameObject.Find("Item Point 2").transform.position,
        };
        difficulty = FindObjectOfType<DifficultyScaling>();
    }

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        const float spawnDelay = 1.5f;
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return new WaitForSeconds(modifiedInterval);
            // add implementation
        }
    }
}
