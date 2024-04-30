using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> slotsBg;
    private int current = 0;
    void Start()
    {
        slotsBg = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            slotsBg.Add(transform.GetChild(i).gameObject);
        }
        Image img = slotsBg[0].GetComponent<Image>();
        Color col = img.color;
        col.a = 0.5f;
        img.color = col;
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int slot_to_change=getPressedKey();
        if (slot_to_change!=-1)
        {
            if(current != slot_to_change)
            {
                Image last_img = slotsBg[current].GetComponent<Image>();
                Color last_col = last_img.color;
                last_col.a = 1f;
                last_img.color = last_col;
            }
            Image img = slotsBg[slot_to_change].GetComponent<Image>();
            Color col = img.color;
            col.a = 0.5f;
            img.color = col;
            current = slot_to_change;
        }
    }

    int getPressedKey()
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
