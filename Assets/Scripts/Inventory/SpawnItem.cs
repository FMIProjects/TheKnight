using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItem : MonoBehaviour
{
    public void CreateItem(GameObject itemPrefab)
    {
        GameObject item = Instantiate(itemPrefab);
        DragDropScript dragDropOriginal = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.childCount != 0)
            {
                dragDropOriginal = child.GetChild(0).GetComponent<DragDropScript>();

                if (item.tag == child.GetChild(0).gameObject.tag && !dragDropOriginal.IsFull(1))
                {
                    dragDropOriginal.RefreshCount(1);
                    Destroy(item);
                    return;
                }
            }
        }

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
