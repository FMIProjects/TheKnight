using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTool : MonoBehaviour
{

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        Harvestable harvestable = other.GetComponent<Harvestable>();

        if (harvestable != null)
        {
            harvestable.Harvest();
        }
    }
}
