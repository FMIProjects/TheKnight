using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAttack : MonoBehaviour
{
    public UnityEvent onAttack;
    

    public void AttackTrigger()
    {
        
        if(onAttack != null)
        {     
            onAttack.Invoke();
        }
            
    }
}