using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItem : MonoBehaviour
{
    public void CreateItem(GameObject itemPrefab)
    {
        // Instantiate the item prefab
        GameObject item = Instantiate(itemPrefab);
        DragDropScript dragDropOriginal = null;

        // Check if the item is already in the inventory
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the child of the inventory slot
            Transform child = transform.GetChild(i);

            // Check if the child has a child
            if (child.childCount != 0)
            {   
                // Get the dragdrop script of the item that is already in the slot
                dragDropOriginal = child.GetChild(0).GetComponent<DragDropScript>();

                // If the item that is being spawned is the same as the item that is already in the slot and the slot is not full
                if (item.tag == child.GetChild(0).gameObject.tag && !dragDropOriginal.IsFull(1))
                {
               
                    dragDropOriginal.RefreshCount(1);
                    Destroy(item);
                    return;
                }
            }
        }
        // If the item is not in the inventory, add it to the first empty slot
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.childCount == 0)
            {
                item.transform.SetParent(child);
                item.transform.localScale = Vector3.one;
                break;
            }
        }
    }
}
