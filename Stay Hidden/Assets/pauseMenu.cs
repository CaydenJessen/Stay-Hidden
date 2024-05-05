using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;
        
        // Show the pause menu UI
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the pause menu UI
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
