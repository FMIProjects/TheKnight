using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Sword,
    Axe,
    Pickaxe
}


public class ResourceTool : MonoBehaviour
{
    [SerializeField] private ToolType toolType;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Harvestable harvestable = other.GetComponent<Harvestable>();
        
        if (harvestable != null && harvestable.GetToolType() == toolType)
        {
            harvestable.Harvest();
        }
    }
}
