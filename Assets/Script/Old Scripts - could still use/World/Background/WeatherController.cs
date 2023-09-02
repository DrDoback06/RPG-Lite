using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private GameObject rainPrefab; // Reference to the rain particle system prefab
    [SerializeField] private GameObject snowPrefab; // Reference to the snow particle system prefab
    [SerializeField] private GameObject windPrefab; // Reference to the wind particle system prefab
    [SerializeField] private GameObject sunPrefab; // Reference to the sun prefab

    [SerializeField] private float weatherChangeInterval = 30f; // Time interval to change the weather
    [SerializeField] private float minDuration = 10f; // Minimum duration for the weather pattern
    [SerializeField] private float maxDuration = 180f; // Maximum duration for the weather pattern
    private float timer; // Timer for tracking the time
    private float duration; // Duration of the current weather pattern
    [SerializeField] private Camera mainCamera;

    private List<GameObject> weatherSystems; // List to store all the active weather systems

    // Enum for different weather types
    private enum WeatherType
    {
        Rain,
        Snow,
        Wind,
        Sun
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the list of active weather systems
        weatherSystems = new List<GameObject>();

        // Set the initial timer and duration
        timer = Time.time;
        duration = Random.Range(minDuration, maxDuration);
    }

    private Vector3 CalculateOffScreenSpawnPosition()
    {
        float offset = 5f; // Set an offset to spawn the particles a certain distance away from the camera view
        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, mainCamera.nearClipPlane)); // Calculate the spawn position at the top edge of the camera view
        spawnPosition += mainCamera.transform.up * offset; // Add the offset to the spawn position in the camera's up direction
        return spawnPosition;
    }


    // Update is called once per frame
    void Update()
    {
        // Check if it's time to change the weather
        if (Time.time - timer > duration)
        {
            // Deactivate the current weather systems
            DeactivateWeatherSystems();

            // Randomly choose a weather type
            WeatherType randomWeather = (WeatherType)Random.Range(0, 4);

            // Call the corresponding function to activate the weather system
            switch (randomWeather)
            {
                case WeatherType.Rain:
                    ActivateWeatherSystem(rainPrefab);
                    break;
                case WeatherType.Snow:
                    ActivateWeatherSystem(snowPrefab);
                    break;
                case WeatherType.Wind:
                    ActivateWeatherSystem(windPrefab);
                    break;
                case WeatherType.Sun:
                    ActivateWeatherSystem(sunPrefab);
                    break;
            }

            // Set a new duration for the weather pattern
            duration = Random.Range(minDuration, maxDuration);

            // Reset the timer
            timer = Time.time;
        }

        // Move the weather systems to follow the attached gameobject on the x axis
        for (int i = 0; i < weatherSystems.Count; i++)
        {
            GameObject weatherSystem = weatherSystems[i];
            if (weatherSystem != null)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = weatherSystem.transform.position.y; // Keep the current y position
                weatherSystem.transform.position = newPosition;
            }
            else
            {
                // Remove the null object from the list
                weatherSystems.RemoveAt(i);
                i--; // Decrease the index to account for the removed element
            }
        }
    }

    void ActivateWeatherSystem(GameObject weatherPrefab)
    {
        // Calculate the off-screen spawn position
        Vector3 spawnPosition = CalculateOffScreenSpawnPosition();

        // Instantiate the weather system and add it to the list of active systems
        GameObject weatherSystem = Instantiate(weatherPrefab, spawnPosition, Quaternion.identity);
        weatherSystems.Add(weatherSystem);

        // Set the duration for the weather system
        ParticleSystem particleSystem = weatherSystem.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Stop(); // Stop the particle system to set the duration
            var main = particleSystem.main;
            main.duration = duration; // Set the duration of the particle system
            particleSystem.Play(); // Restart the particle system
        }

        // Destroy the weather system after the duration has elapsed
        Destroy(weatherSystem, duration);
    }


    // Function to deactivate and destroy all active weather systems
    void DeactivateWeatherSystems()
    {
        // Loop through all the active weather systems and destroy them
        foreach (GameObject weatherSystem in weatherSystems)
        {
            Destroy(weatherSystem);
        }
        // Clear the list of active weather systems
        weatherSystems.Clear();
    }
}

// Class to handle destroying the weather system after a specified duration
public class DestroyAfterDuration : MonoBehaviour
{
    public float duration; // Duration after which the gameobject will be destroyed
                           // Start is called before the first frame update
    void Start()
    {
        // Destroy the gameobject after the specified duration
        Destroy(gameObject, duration);
    }
}