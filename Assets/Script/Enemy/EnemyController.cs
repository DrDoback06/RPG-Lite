using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int experienceValue = 50;
    public int level;

    public delegate void EnemyDefeatedHandler(int experienceValue);
    public event EnemyDefeatedHandler OnEnemyDefeated;

    public void SetLevel(int newLevel)
    {
        level = newLevel;

        // Update the enemy's stats or attributes based on the new level
        // ...
    }

    public int GetScaledExperiencePoints(int playerLevel)
    {
        int levelDifference = playerLevel - level;
        float scalingFactor = Mathf.Clamp(1f + 0.1f * levelDifference, 0.5f, 2f);
        return Mathf.RoundToInt(experienceValue * scalingFactor);
    }

    // Call this method when the enemy is defeated (e.g., when its health reaches 0)
    public void DefeatEnemy(int playerLevel)
    {
        int scaledExperiencePoints = GetScaledExperiencePoints(playerLevel);

        // Raise the event and pass the scaled experience value as an argument
        OnEnemyDefeated?.Invoke(scaledExperiencePoints);

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
