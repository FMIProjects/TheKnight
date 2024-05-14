using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public int gameStartScene;

    [SerializeField] private Button continueButton;

    [SerializeField] private Button loadGameButton;

    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    private void Start()
    {
       if(!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        saveSlotsMenu.ActivateMenu(false);
       this.DeactivateMenu();
    }

    public void LoadGame()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync(gameStartScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateMenu()
    {
       this.transform.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.transform.gameObject.SetActive(false);
    }
}
