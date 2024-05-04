using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Harvestable : MonoBehaviour
{
    [SerializeField] private int amount = 8;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private ToolType harvestableType;
    public void Harvest()
    {
        --amount;
        if(amount <= 0)
        {
            Destroy(gameObject);
        }

        var spawner = inventory.GetComponent<SpawnItem>();

        if (spawner != null)
        {
            spawner.CreateItem(droppedItemPrefab);
            Debug.Log("success");

        }


    }

    public ToolType GetToolType()
    {
        return harvestableType;
    }

    public void SetInventory(GameObject inventory)
    {
        this.inventory = inventory;
    }   
}
