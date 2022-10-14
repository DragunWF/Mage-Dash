using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float[] spawnIntervals = new float[2] { 1.5f, 2.5f };
    private bool spawnObstacles = true;

    private GameObject[] groundObstacles;
    private GameObject flyingObstacle;

    private Transform flyingObstacleSpawnPoint;
    private Transform groundObstacleSpawnPoint;

    public void ScaleSpawner()
    {
        UpdateSpawnIntervals();
    }

    private void Awake()
    {
        flyingObstacle = Resources.Load("Prefabs/Obstacle [Bat]") as GameObject;

        groundObstacleSpawnPoint = GameObject.Find("Ground Enemy Point").transform;
        flyingObstacleSpawnPoint = GameObject.Find("Flying Enemy Point").transform;
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
        int chance = Random.Range(1, 2);
        if (chance == 1)
            return flyingObstacle;
        return groundObstacles[Random.Range(0, groundObstacles.Length)];
    }

    private IEnumerator SpawnObstacles()
    {
        const float spawnDelay = 1.5f;
        yield return new WaitForSeconds(spawnDelay);

        while (spawnObstacles)
        {
            float spawnInterval = GetRandomSpawnInterval();
            GameObject obstacle = GetRandomObstacle();
            Transform spawnPosition = obstacle == flyingObstacle ?
                flyingObstacleSpawnPoint : groundObstacleSpawnPoint;

            Instantiate(obstacle, spawnPosition);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
