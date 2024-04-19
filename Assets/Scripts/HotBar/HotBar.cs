using UnityEngine;
using System.Collections.Generic;
public class Hotbar : MonoBehaviour
{
    private List<GameObject> slots;
    private int currentSlotIndex = 0;

    void Start()
    {

        slots = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;

            slots.Add(childObject);
        }

        if(slots.Count > 0)
        {
            Debug.Log(slots.Count); 

            for (int i = 1; i < slots.Count; i++)
            {
                slots[i].SetActive(false);
            }
        }

      
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSlot(3);
        }
    }

    void ChangeSlot(int newIndex)
    {
       
        slots[currentSlotIndex].SetActive(false);

        currentSlotIndex = newIndex;

        slots[currentSlotIndex].SetActive(true);
    }
}
