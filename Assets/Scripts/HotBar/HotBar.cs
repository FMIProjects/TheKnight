using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    // takes the canvas horbar object
    [SerializeField] private GameObject HotBarCanvas;

    private List<GameObject> slots;
    private int currentSlotIndex = 0;
    
    void Start()
    {

        slots = new List<GameObject>();

        // get all the children of the parent ( the player)
        // meaning get all the ToolParent
        for (int i = 0; i < transform.childCount; i++)
        {
            // get the current ToolParent
            GameObject childObject = transform.GetChild(i).gameObject;

            // get the canvasSlot mapped to the current tool
            GameObject canvasSlot = HotBarCanvas.transform.GetChild(i).gameObject;
            // get the canvas sprite
            var canvasImage = canvasSlot.GetComponent<Image>();

            // if the current ToolParent has a tool
            if(childObject.transform.childCount > 0)
            {
                // get that tool
                var childOfChild = childObject.transform.GetChild(0).gameObject;

                // get the tool sprite
                var childImage = childOfChild.GetComponent<SpriteRenderer>().sprite;
                // assign the tool sprite to the slot sprite
                canvasImage.sprite = childImage;

            }

            slots.Add(childObject);
        }

        // set to false all the other slots except the first
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
        // test if the indexdoes not go out of bound
        if (slots.Count <= newIndex)
            return;
        
        // deactivate current object
        slots[currentSlotIndex].SetActive(false);

        // update slot index
        currentSlotIndex = newIndex;
        
        // activate new object
        slots[currentSlotIndex].SetActive(true);


    }
}
