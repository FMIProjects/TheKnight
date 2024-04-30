using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    private bool isInInventory;
    KnightController knightController;

    PauseMenu pauseMenu;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject spawnItemButton;
    // Start is called before the first frame update
    void Start()
    {
        isInInventory = false;
        inventory.SetActive(false);
        knightController = GetComponent<KnightController>();
        pauseMenu = inventory.GetComponentInParent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            enableInventory();
        }
        else if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S))
        {
            disableInventory();
        }
        
    }

    public void enableInventory()
    {
        if (isInInventory == false && pauseMenu.gameIsPaused == false)
        {
            isInInventory = true;
            knightController.enabled = false;
            inventory.SetActive(true);
            spawnItemButton.SetActive(true);
        }
        else
        {
            isInInventory = false;
            knightController.enabled = true;
            inventory.SetActive(false);
            spawnItemButton.SetActive(false);
        }
    }

    public void disableInventory()
    {

        isInInventory = false;
        knightController.enabled = true;
        inventory.SetActive(false);
        spawnItemButton.SetActive(false);
    }

}
