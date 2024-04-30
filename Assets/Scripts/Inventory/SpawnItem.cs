using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItem : MonoBehaviour
{
    public void createItem(GameObject itemPrefab)
    {
        GameObject item = Instantiate(itemPrefab);
        item.GetComponent<DragDropScript>().img = item.GetComponent<Image>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                DragDropScript dragDropOriginal = transform.GetChild(i).transform.GetChild(0).GetComponent<DragDropScript>();

                if (item.tag == transform.GetChild(i).transform.GetChild(0).gameObject.tag && !dragDropOriginal.isFull(1))
                {
                    dragDropOriginal.refreshCount(1);
                    Destroy(item);
                    return;
                }
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                item.transform.SetParent(transform.GetChild(i).transform);
                item.transform.localScale = Vector3.one;
                break;
            }
        }
    }
}
