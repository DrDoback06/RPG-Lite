using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas[] uiCanvases; // Add the Canvas array here

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Keep all the canvases across scenes
            foreach (Canvas canvas in uiCanvases)
            {
                DontDestroyOnLoad(canvas.gameObject);
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
