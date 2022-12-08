using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PowerupSpawner : MonoBehaviour
{
    private float[] spawnIntervals = { 8.5f, 9f, 9.5f, 10f, 10.5f, 11f };
    private GameObject[] powerups;
    private Transform[] spawnPositions;
    private Player player;

    private void Awake()
    {
        powerups = new GameObject[4] {
            Resources.Load("Prefabs/HealthPotion") as GameObject,
            Resources.Load("Prefabs/ManaPotion") as GameObject,
            Resources.Load("Prefabs/DoubleScorePotion") as GameObject,
            Resources.Load("Prefabs/DamagePotion") as GameObject
        };
        spawnPositions = new Transform[2] {
            GameObject.Find("Item Ground Point").transform,
            GameObject.Find("Item Upper Point").transform
        };
        player = FindObjectOfType<Player>();
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

    private Object GetRandomItem(Object[] array)
    {
        int randomIndex = GetRandomIndex(array.Length);
        return array[randomIndex];
    }

    private GameObject GetDistinctPowerup()
    {
        if (player.GetScorePowerupStatus() &&
            player.GetManaPowerupStatus() &&
            player.GetDamagePowerupStatus())
        {
            return powerups[0]; // health powerup
        }

        GameObject powerup = null;
        bool foundDistinct = false;

        while (!foundDistinct)
        {
            powerup = GetRandomItem(powerups) as GameObject;

            switch (powerup.GetComponent<Powerup>().GetPotionType())
            {
                case "health":
                    foundDistinct = true;
                    break;
                case "mana":
                    if (!player.GetManaPowerupStatus())
                        foundDistinct = true;
                    break;
                case "score":
                    if (!player.GetScorePowerupStatus())
                        foundDistinct = true;
                    break;
                case "damage":
                    if (!player.GetDamagePowerupStatus())
                        foundDistinct = true;
                    break;
            }
        }

        return powerup;
    }

    private IEnumerator SpawnPowerups()
    {
        const float spawnDelayTime = 2.5f;
        yield return new WaitForSeconds(spawnDelayTime);

        while (true)
        {
            yield return new WaitForSeconds(GetRandomInterval());

            GameObject powerup = GetDistinctPowerup();
            if (powerup != null)
            {
                Transform spawnTransform = GetRandomItem(spawnPositions) as Transform;
                Instantiate(powerup, spawnTransform.position, Quaternion.identity);
            }
        }
    }
}
