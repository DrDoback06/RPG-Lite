using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int minRooms;
    public int maxRooms;

    public GameObject[] roomPrefabs;
    public GameObject[] corridorPrefabs;

    public void GenerateDungeon(int characterLevel)
    {
        int numberOfRooms = Random.Range(minRooms, maxRooms + 1);
        float offsetX = 0.0f;

        for (int i = 0; i < numberOfRooms; i++)
        {
            GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
            GameObject corridorPrefab = corridorPrefabs[Random.Range(0, corridorPrefabs.Length)];

            GameObject roomInstance = Instantiate(roomPrefab, new Vector3(offsetX, 0, 0), Quaternion.identity);
            offsetX += roomInstance.GetComponent<SpriteRenderer>().bounds.size.x;

            if (i < numberOfRooms - 1)
            {
                GameObject corridorInstance = Instantiate(corridorPrefab, new Vector3(offsetX, 0, 0), Quaternion.identity);
                offsetX += corridorInstance.GetComponent<SpriteRenderer>().bounds.size.x;
            }
        }
    }
}
