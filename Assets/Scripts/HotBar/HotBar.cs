using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
public class Hotbar : MonoBehaviour
{
    [SerializeField] private GameObject HotBarCanvas;

    private List<GameObject> slots;
    private int currentSlotIndex = 0;
    private ItemParent sword;

    void Start()
    {
        slots = new List<GameObject>();
        sword = GetComponentInChildren<ItemParent>();

        InitializeSlots();
        SetActiveSlot(0);

        // Set all the other slots except the first to false
        if (slots.Count > 0)
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
        bool isAttacking = sword.CheckAttacking();
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Wait(.1f);
                SetActiveSlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Wait(.1f);
                SetActiveSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Wait(.1f);
                SetActiveSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Wait(.1f);
                SetActiveSlot(3);
            }
        }
    }

    void InitializeSlots()
    {
        // Get all the children of the parent (the player)
        // Meaning get all the ToolParent
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the current ToolParent
            GameObject childObject = transform.GetChild(i).gameObject;

            // Get the canvasSlot mapped to the current tool
            GameObject canvasSlot = HotBarCanvas.transform.GetChild(i + 4).gameObject;

            // Get the canvas sprite
            var canvasImage = canvasSlot.GetComponent<Image>();

            // If the current ToolParent has a tool
            if (childObject.transform.childCount > 0)
            {
                // Get that tool
                var childOfChild = childObject.transform.GetChild(0).gameObject;

                // Get the tool sprite
                var childImage = childOfChild.GetComponent<SpriteRenderer>().sprite;

                // Assign the tool sprite to the slot sprite
                canvasImage.sprite = childImage;
            }

            slots.Add(childObject);
        }
    }

    void SetActiveSlot(int newIndex)
    {
        if (slots.Count <= newIndex)
            return;

        slots[currentSlotIndex].SetActive(false);
        currentSlotIndex = newIndex;
        slots[currentSlotIndex].SetActive(true);
    }

    private IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }
}
