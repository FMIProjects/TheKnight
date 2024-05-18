using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int steps)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        
        var previousPosition = startPosition;

        for(int i=0; i< steps; i++)
        {
            // generate take a step in a random direction
            var newPosition = previousPosition + Direction2D.GetRandomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardianlDirections = new List<Vector2Int>
        {
            new Vector2Int(0,1), //UP
            new Vector2Int(0,-1), //DOWN
            new Vector2Int(1,0), //RIGHT
            new Vector2Int(-1,0) //LEFT
        };

        public static Vector2Int GetRandomDirection()
        {
            
            int randomIndex = UnityEngine.Random.Range(0, cardianlDirections.Count);

            return cardianlDirections[randomIndex];

        }
    }

    
}
