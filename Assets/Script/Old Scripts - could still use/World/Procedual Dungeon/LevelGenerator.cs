using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject groundPrefab;
    public int minLevelLength = 50;
    public int maxLevelLength = 150;

    public int levelLength;

    private void Start()
    {
        levelLength = Random.Range(minLevelLength, maxLevelLength + 1);
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < levelLength; i++)
        {
            Instantiate(groundPrefab, new Vector3(i, 0, 0), Quaternion.identity, transform);
        }
    }
}
