using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public int gameStartScene;

    public GameObject pauseMenuUI;

    public GameObject mainCamera;

    PostProcessVolume ppVolume;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject knight;


    private void Start()
    {
        ppVolume = mainCamera.GetComponent<PostProcessVolume>();
        ppVolume.enabled = false;
        pauseMenuUI.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                
                Resume();
            }
            else
            {
                knight.GetComponent<InventoryController>().disableInventory();
                Pause();
            }
        }
    }

    public void Resume()
    {
        ppVolume.enabled = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        ppVolume.enabled = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameStartScene);
    }

    public void QuitGame()
    {
       Application.Quit();
    }
}
