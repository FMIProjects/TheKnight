using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private bool isInInventory;
    private KnightController knightController;
    private PauseMenu pauseMenu;

    // canvas inventory component and spawn item button
    [SerializeField] private GameObject inventory;

    void Start()
    {
        // Set initial state
        isInInventory = false;

        inventory.SetActive(false);

        // Get references to other components
        knightController = GetComponent<KnightController>();
        pauseMenu = inventory.GetComponentInParent<PauseMenu>();

    }

    void Update()
    {
        // Handle inventory input
        HandleInventoryInput();
    }

    public void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Toggle inventory
            ToggleInventory();
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            // Disable inventory
            DisableInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!isInInventory && !pauseMenu.gameIsPaused)
        {
            // Enable inventory
            EnableInventory();
        }
        else
        {
            // Disable inventory
            DisableInventory();
        }
    }

    public void EnableInventory()
    {
        isInInventory = true;

        knightController.enabled = false;

        inventory.SetActive(true);
    }

    public void DisableInventory()
    {
        isInInventory = false;

        knightController.enabled = true;

        inventory.SetActive(false);
    }
}
