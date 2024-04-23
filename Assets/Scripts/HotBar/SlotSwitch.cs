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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(current != 0)
            {
                Image last_img = slotsBg[current].GetComponent<Image>();
                Color last_col = last_img.color;
                last_col.a = 1f;
                last_img.color = last_col;
            }
            Image img = slotsBg[0].GetComponent<Image>();
            Color col = img.color;
            col.a = 0.5f;
            img.color = col;
            current = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (current != 1)
            {
                Image last_img = slotsBg[current].GetComponent<Image>();
                Color last_col = last_img.color;
                last_col.a = 1f;
                last_img.color = last_col;
            }
            Image img = slotsBg[1].GetComponent<Image>();
            Color col = img.color;
            col.a = 0.5f;
            img.color = col;
            current = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (current != 2)
            {
                Image last_img = slotsBg[current].GetComponent<Image>();
                Color last_col = last_img.color;
                last_col.a = 1f;
                last_img.color = last_col;
            }
            Image img = slotsBg[2].GetComponent<Image>();
            Color col = img.color;
            col.a = 0.5f;
            img.color = col;
            current = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (current != 3)
            {
                Image last_img = slotsBg[current].GetComponent<Image>();
                Color last_col = last_img.color;
                last_col.a = 1f;
                last_img.color = last_col;
            }
            Image img = slotsBg[3].GetComponent<Image>();
            Color col = img.color;
            col.a = 0.5f;
            img.color = col;
            current = 3;
        }
    }
}
