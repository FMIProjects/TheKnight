using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public int gameStartScene;
    public GameObject pauseMenuUI;
    public GameObject mainCamera;

    private PostProcessVolume ppVolume;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject knight;

    private void Start()
    {
        ppVolume = mainCamera.GetComponent<PostProcessVolume>();
        ppVolume.enabled = false;
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                knight.GetComponent<InventoryController>().DisableInventory();
                Pause();
            }
        }
    }

    // Resumes the game by disabling the post-processing volume, hiding the pause menu UI, and setting the time scale to normal.
    public void Resume()
    {
        ppVolume.enabled = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Pauses the game by enabling the post-processing volume, showing the pause menu UI, and setting the time scale to 0.
    private void Pause()
    {
        ppVolume.enabled = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Loads the main menu scene by setting the time scale to normal and loading the specified game start scene.
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameStartScene);
    }

    // Quits the game.
    public void QuitGame()
    {
        Application.Quit();
    }
}
