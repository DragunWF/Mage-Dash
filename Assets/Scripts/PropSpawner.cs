using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PropSpawner : MonoBehaviour
{
    private float[] spawnIntervals = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f };
    private Transform spawnPoint;
    private GameObject prop;

    public void RestartSpawning()
    {
        StartCoroutine(SpawnProps());
    }

    private void Awake()
    {
        prop = Resources.Load("Prefabs/Cactus") as GameObject;
        spawnPoint = GameObject.Find("Prop Point").transform;
    }

    private void Start()
    {
        StartCoroutine(SpawnProps());
    }

    private float GetRandomInterval()
    {
        return spawnIntervals[Random.Range(0, spawnIntervals.Length)];
    }

    private IEnumerator SpawnProps()
    {
        const float initialSpawnDelay = 1.5f;
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            float interval = GetRandomInterval();
            yield return new WaitForSeconds(interval);
            Instantiate(prop, spawnPoint, true);
            yield return new WaitForSeconds(interval);
        }
    }
}
