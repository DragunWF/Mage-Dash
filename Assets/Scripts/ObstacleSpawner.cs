using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float[] spawnIntervals = { 1.5f, 2.5f };
    private bool spawnObstacles = true;

    private GameObject[] groundObstacles;
    private GameObject flyingObstacle;

    private Transform flyingSpawnPoint;
    private Transform groundSpawnPoint;

    public void ScaleSpawner()
    {
        UpdateSpawnIntervals();
    }

    private void Awake()
    {
        groundObstacles = new GameObject[3] {
            Resources.Load("Prefabs/Obstacle [Spike]") as GameObject,
            Resources.Load("Prefabs/Obstacle [Spider]") as GameObject,
            Resources.Load("Prefabs/Obstacle [Fighter]") as GameObject
        };
        flyingObstacle = Resources.Load("Prefabs/Obstacle [Bat]") as GameObject;

        groundSpawnPoint = GameObject.Find("Ground Enemy Point").transform;
        flyingSpawnPoint = GameObject.Find("Flying Enemy Point").transform;
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private void UpdateSpawnIntervals()
    {

    }

    private float GetRandomSpawnInterval()
    {
        float minSpawnInterval = spawnIntervals[0];
        float maxSpawnInterval = spawnIntervals[1];

        List<float> spawnIntervalsList = new List<float>();
        float interval = minSpawnInterval;
        while (interval < maxSpawnInterval)
        {
            spawnIntervalsList.Add(interval);
            interval += 0.1f;
        }

        return spawnIntervalsList[Random.Range(0, spawnIntervalsList.Count)];
    }

    private GameObject GetRandomObstacle()
    {
        int chance = Random.Range(1, 3);
        if (chance == 1)
        {
            return flyingObstacle;
        }
        return groundObstacles[Random.Range(0, groundObstacles.Length)];
    }

    private IEnumerator SpawnObstacles()
    {
        const float spawnDelay = 3.5f;
        yield return new WaitForSeconds(spawnDelay);

        while (spawnObstacles)
        {
            float spawnInterval = GetRandomSpawnInterval();
            GameObject obstacle = GetRandomObstacle();
            Transform spawnPosition = obstacle == flyingObstacle ?
                flyingSpawnPoint : groundSpawnPoint;

            Instantiate(obstacle, spawnPosition, false);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
