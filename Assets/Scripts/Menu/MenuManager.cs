using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public int gameStartScene;

    [SerializeField] private Button continueButton;

    private void Start()
    {
       if(!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(gameStartScene);
    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync(gameStartScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
