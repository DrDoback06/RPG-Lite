using UnityEngine;

public class DungeonSpawningSystem : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public GameObject[] enemyPrefabs;
    public int minEnemiesPerRoom;
    public int maxEnemiesPerRoom;
    public int spawnChance = 50; // Changed to int and set to 50 for a 50% chance

    private void Start()
    {
        Character character = CharacterManager.Instance.character;
        dungeonGenerator.GenerateDungeon(character.level);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        int characterLevel = CharacterManager.Instance.character.level;

        foreach (Room room in rooms)
        {
            if (Random.Range(0, 101) <= spawnChance) // Updated comparison to use Random.Range and check against spawnChance
            {
                int enemiesInRoom = Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom + 1);

                for (int i = 0; i < enemiesInRoom; i++)
                {
                    GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                    Vector3 spawnPosition = room.GetSpawnPoint();

                    GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                    // Set enemy level based on character level
                    EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();
                    enemyController.SetLevel(characterLevel);
                }
            }
        }
    }
}
