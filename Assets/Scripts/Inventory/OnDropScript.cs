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
            
            if (transform.GetChild(0).name == eventData.pointerDrag.name && !dragDropOriginal.isFull(dragDrop.getCount()) && !dragDrop.isFull(dragDropOriginal.getCount()))
            {
                
                
                dragDropOriginal.refreshCount(dragDrop.getCount());
                Destroy(dragDrop.gameObject);
                Debug.Log(dragDropOriginal.getCount());
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
