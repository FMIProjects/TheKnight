using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropScript : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        DragDropScript dragDrop = eventData.pointerDrag.GetComponent<DragDropScript>();
        
        if (transform.childCount != 0 )
        {
            DragDropScript dragDropOriginal = transform.GetChild(0).GetComponent<DragDropScript>();
            
            if (transform.GetChild(0).name == eventData.pointerDrag.name && !dragDropOriginal.IsFull(dragDrop.GetCount()) && !dragDrop.IsFull(dragDropOriginal.GetCount()))
            {
                dragDropOriginal.RefreshCount(dragDrop.GetCount());
                Destroy(dragDrop.gameObject);
                Debug.Log(dragDropOriginal.GetCount());
                return;
            }
            else
            {
                dragDropOriginal.transform.SetParent(dragDrop.originalPos);
            }
        }
        dragDrop.originalPos = transform;
    }
}
