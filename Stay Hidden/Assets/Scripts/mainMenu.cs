using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Declare the pause menu UI object
    private bool isPaused = false;

  void Start()
    {
        // Ensure the pause menu is initially hidden
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    // Method to start the game
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Test Level");
          
    }

    // Method to exit the application
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }

  void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
           TogglePauseMenu();
        }
    }

    // Method to toggle the pause menu
    private void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        // Show the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    // Method to resume the game
    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

    }
    
}

