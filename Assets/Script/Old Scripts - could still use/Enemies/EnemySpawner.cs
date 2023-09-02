using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyTier> enemyTiers;
    public List<int> numberOfEnemiesPerTier;
    public Transform levelParent;
    public int numberOfEnemies = 10;
    public float minSpawnHeight = 1f;
    public float maxSpawnHeight = 5f;
    public float minimumDistance = 1f;

    private List<Vector3> spawnPoints = new List<Vector3>();

    private void Start()
    {
        GenerateSpawnPoints();
        SpawnEnemiesByTier();
    }

    private void SpawnEnemiesByTier()
    {
        for (int i = 0; i < enemyTiers.Count; i++)
        {
            EnemyTier tier = enemyTiers[i];
            int numberOfEnemiesForTier = numberOfEnemiesPerTier[i];

            for (int j = 0; j < numberOfEnemiesForTier; j++)
            {
                Vector3 spawnPoint = GetRandomValidSpawnPoint();
                GameObject spawnedEnemy = Instantiate(tier.enemyPrefab, spawnPoint, Quaternion.identity, levelParent);
                EnemyLevel enemyLevel = spawnedEnemy.GetComponent<EnemyLevel>();
                if (enemyLevel != null)
                {
                    enemyLevel.level = Random.Range(tier.minLevel, tier.maxLevel + 1);
                }
            }
        }
    }

    private Vector3 GetRandomValidSpawnPoint()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnPoints.Remove(spawnPoint);
        return spawnPoint;
    }

    private void GenerateSpawnPoints()
    {
        int levelLength = levelParent.GetComponent<LevelGenerator>().levelLength;
        int attempts = 0;

        while (spawnPoints.Count < numberOfEnemies && attempts < 1000)
        {
            float x = Random.Range(1, levelLength);
            float y = Random.Range(minSpawnHeight, maxSpawnHeight);
            Vector3 potentialSpawnPoint = new Vector3(x, y, 0);

            if (IsSpawnPointValid(potentialSpawnPoint))
            {
                spawnPoints.Add(potentialSpawnPoint);
            }

            attempts++;
        }
    }

    private bool IsSpawnPointValid(Vector3 potentialSpawnPoint)
    {
        foreach (Vector3 existingSpawnPoint in spawnPoints)
        {
            if (Vector3.Distance(existingSpawnPoint, potentialSpawnPoint) < minimumDistance)
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnEnemies()
    {
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            EnemyTier selectedTier = SelectEnemyTier();
            GameObject spawnedEnemy = Instantiate(selectedTier.enemyPrefab, spawnPoint, Quaternion.identity, levelParent);
            EnemyLevel enemyLevel = spawnedEnemy.GetComponent<EnemyLevel>();
            if (enemyLevel != null)
            {
                enemyLevel.level = Random.Range(selectedTier.minLevel, selectedTier.maxLevel + 1);
            }
        }
    }


    private EnemyTier SelectEnemyTier()
    {
        int totalWeight = 0;
        foreach (EnemyTier tier in enemyTiers)
        {
            totalWeight += tier.spawnWeight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int weightSum = 0;

        foreach (EnemyTier tier in enemyTiers)
        {
            weightSum += tier.spawnWeight;
            if (randomWeight < weightSum)
            {
                return tier;
            }
        }

        return null;
    }
}
