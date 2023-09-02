using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform spawnPoint;

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint.position;
    }
}
