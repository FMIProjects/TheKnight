using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSwitch : MonoBehaviour
{
    private List<GameObject> slotsBg;
    private int current = 0;

    void Start()
    {
        InitializeSlots();
        SetSlotColor(0, 0.5f);
    }

    void Update()
    {   
        // get the player and the horbar
        GameObject player = GameObject.FindWithTag("Player");
        Hotbar hotBar = player?.GetComponent<Hotbar>();

        // check if the player is attacking
        bool isAttacking = hotBar?.GetCurrentToolParent().CheckAttacking() ?? false;

        int slotToChange = GetPressedKey();

        if (!isAttacking && slotToChange != -1 && current != slotToChange)
        {
            SetSlotColor(current, 1f);
            SetSlotColor(slotToChange, 0.5f);
            current = slotToChange;
        }
    }

    void InitializeSlots()
    {
        slotsBg = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            slotsBg.Add(transform.GetChild(i).gameObject);
        }
    }

    void SetSlotColor(int slotIndex, float alpha)
    {
        Image img = slotsBg[slotIndex].GetComponent<Image>();
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }

    int GetPressedKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 3;
        }
        return -1;
    }
}
