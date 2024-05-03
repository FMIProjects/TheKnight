using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceData : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject prefab;
    

    void Start()
    {
        StartCoroutine(AsteaptaCinciSecunde());
    }

    IEnumerator AsteaptaCinciSecunde()
    {
        
        yield return new WaitForSeconds(5f);


        var spawner = inventory.GetComponent<SpawnItem>();

        if (spawner != null)
        {
            spawner.CreateItem(prefab);
            Debug.Log("success");

        }
    }

}
