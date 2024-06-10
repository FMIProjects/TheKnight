using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterManager : MonoBehaviour
{

    private int maxEnemies = 0;
    private int currentNumberOfEnemies = 0;
    GameObject enemyCounter = null;
    
    void Start()
    {
        // get the counter
        enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter");
   
    }

    void Update()
    {
        
        // set the max number of enemies once, does not work in start
        if(maxEnemies == 0)
        {
            maxEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        // get the current number of enemies
        currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Text textComponent = enemyCounter.GetComponent<Text>();

        // update the text
        textComponent.text = currentNumberOfEnemies.ToString();

        // change the color of the text if the number of enemies is less than half of the max number
        // needs to be different from 0 to avoid the case when the enemies have not been spawned yet
        if(currentNumberOfEnemies != 0 && currentNumberOfEnemies <= maxEnemies/2)
        {
            textComponent.color = Color.green;
        }
        
        

    }
}
