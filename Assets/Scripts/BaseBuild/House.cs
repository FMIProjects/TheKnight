using System;
using UnityEngine;

public class House : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;
    [SerializeField] private bool isBuilt;

    [SerializeField] private int woodCostPerHouse = 10;
    [SerializeField] private int rockCostPerHouse = 10;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject rockPrefab;

    private Slot[] inventorySlots;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = Guid.NewGuid().ToString();
    }

    void Start()
    {
        inventorySlots = inventory.GetComponentsInChildren<Slot>();

        if (isBuilt)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData.builtHouses.ContainsKey(id))
        {
            isBuilt = gameData.builtHouses[id];
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.builtHouses[id] = isBuilt;
    }

    public void BuildHouse()
    {
        if (!isBuilt)
        {
            if (HasEnoughResources())
            {
                DeductResources();

                isBuilt = true;
                gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Not enough resources to build the house.");
            }
        }
        else
        {
            Debug.Log("House is already built.");
        }
    }

    private bool HasEnoughResources()
    {
        int woodCount = 0;
        int rockCount = 0;

        // Count available wood and rock in the inventory
        foreach (Slot slot in inventorySlots)
        {
            if (slot.itemInfo.itemType == "wood")
            {
                woodCount += Mathf.Min(slot.itemInfo.itemCount, 8); // Max items per slot is 8
            }
            else if (slot.itemInfo.itemType == "rock")
            {
                rockCount += Mathf.Min(slot.itemInfo.itemCount, 8); // Max items per slot is 8
            }
        }

        // Check if the player has enough wood and rock to build the house
        return woodCount >= woodCostPerHouse && rockCount >= rockCostPerHouse;
    }

    private void DeductResources()
    {
        int woodRemaining = woodCostPerHouse;
        int rockRemaining = rockCostPerHouse;

        // Deduct wood from inventory
        foreach (Slot slot in inventorySlots)
        {
            if (slot.itemInfo.itemType == "wood")
            {
                int woodToDeduct = Mathf.Min(slot.itemInfo.itemCount, woodRemaining, 8); // Max items per slot is 8
                slot.itemInfo.itemCount -= woodToDeduct;
                woodRemaining -= woodToDeduct;

                GameObject item = slot.transform.GetChild(0).gameObject;

                DragDropScript dragDropOriginal = item.transform.GetComponent<DragDropScript>();
                dragDropOriginal.RefreshCount(-slot.itemInfo.itemCount);

                if (woodRemaining <= 0)
                {
                    break;
                }
            }
        }

        // Deduct rock from inventory
        foreach (Slot slot in inventorySlots)
        {
            if (slot.itemInfo.itemType == "rock")
            {
                int rockToDeduct = Mathf.Min(slot.itemInfo.itemCount, rockRemaining, 8); // Max items per slot is 8
                slot.itemInfo.itemCount -= rockToDeduct;
                rockRemaining -= rockToDeduct;

                GameObject item = slot.transform.GetChild(0).gameObject;

                DragDropScript dragDropOriginal = item.transform.GetComponent<DragDropScript>();
                dragDropOriginal.RefreshCount(-slot.itemInfo.itemCount);

                if (rockRemaining <= 0)
                {
                    break;
                }
            }
        }


    }

    void ShowSignCanvas(bool show)
    {
        Sign sign = GetComponent<Sign>();
        if (sign != null && sign.signCanvas != null)
        {
            sign.signCanvas.SetActive(show);
        }
    }
}
