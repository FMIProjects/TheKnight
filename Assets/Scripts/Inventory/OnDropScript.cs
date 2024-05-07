using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropScript : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)

    {   // get the dragdrop script of the item that is being dragged
        DragDropScript dragDrop = eventData.pointerDrag.GetComponent<DragDropScript>();
        
        // if the slot is not empty
        if (transform.childCount != 0 )
        {
            // get the dragdrop script of the item that is already in the slot
            DragDropScript dragDropOriginal = transform.GetChild(0).GetComponent<DragDropScript>();
            
            // if the item that is being dragged is the same as the item that is already in the slot and the slot is not full
            if (transform.GetChild(0).name == eventData.pointerDrag.name && !dragDropOriginal.IsFull(dragDrop.GetCount()) && !dragDrop.IsFull(dragDropOriginal.GetCount()))
            {
                // add the count of the item that is being dragged to the item that is already in the slot
                dragDropOriginal.RefreshCount(dragDrop.GetCount());
                // destroy the item that is being dragged
                Destroy(dragDrop.gameObject);
                Debug.Log(dragDropOriginal.GetCount());
                return;
            }
            else
            {
                // if the item that is being dragged is not the same as the item that is already in the slot swap the items
                dragDropOriginal.transform.SetParent(dragDrop.originalPos);
            }
        }
        // set the parent of the item that is being dragged to the slot
        dragDrop.originalPos = transform;
    }
}
