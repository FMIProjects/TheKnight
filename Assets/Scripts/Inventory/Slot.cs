using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct ItemInfo
{
    public int itemCount;
    public string itemType;
}

public class Slot : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;


    [SerializeField] private GameObject wood;

    [SerializeField] private GameObject rock;


    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = Guid.NewGuid().ToString();
    }

    public ItemInfo itemInfo;
    public bool itemEquipped = false;

    public void LoadData(GameData gameData)
    {
        if (gameData.slotsType.ContainsKey(id))
        {
            itemInfo.itemType = gameData.slotsType[id];
            itemEquipped = true;
        }
        
        if(gameData.slotsCount.ContainsKey(id))
            itemInfo.itemCount = Convert.ToInt32(gameData.slotsCount[id]);

        GameObject item = null;

        if(gameData.slotsType.ContainsKey(id) && gameData.slotsCount.ContainsKey(id))
        {
            if (itemInfo.itemType == "wood")
                item = Instantiate(wood);
            else if (itemInfo.itemType == "rock")
                item = Instantiate(rock);

            if(item != null)
            {
                item.transform.SetParent(this.transform);
                item.transform.localScale = Vector3.one;
                DragDropScript dragDropOriginal = item.transform.GetComponent<DragDropScript>();
                dragDropOriginal.RefreshCount(itemInfo.itemCount-1);
            }
        }

    }

    public void SaveData(ref GameData gameData)
    {
        if (itemEquipped)
        {
            Debug.Log("Saving item " + itemInfo.itemType + " with count " + itemInfo.itemCount);
            gameData.slotsType[id] = itemInfo.itemType;
            gameData.slotsCount[id] = itemInfo.itemCount;
        }
        else
        {
            gameData.slotsType.Remove(id);
            gameData.slotsCount.Remove(id);
        }

    }


    void Update()
    {
        if (transform.childCount > 0)
        {
            itemEquipped = true;
            itemInfo.itemType = transform.GetChild(0).name.Split("(")[0];
            itemInfo.itemCount = Int32.Parse(transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
        }
        else
        {
            itemEquipped = false;
            itemInfo.itemCount = 0;
            itemInfo.itemType = "";
        }
    }
}