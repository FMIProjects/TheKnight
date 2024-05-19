using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkSO", menuName = "ProceduralGenerationSO/SimpleRandomWalkSO")]
public class SimpleRandomWalkSO : ScriptableObject 
{

    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomly = true;
}
