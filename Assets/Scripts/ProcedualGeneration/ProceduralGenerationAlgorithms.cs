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

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int statPosition, int corridorLength)
    {
        // generate a corridor of a given length
        List<Vector2Int> corridorPositions = new List<Vector2Int>();
        // get a random direction
        var direction = Direction2D.GetRandomDirection();
        var currentPosition = statPosition;
        corridorPositions.Add(currentPosition);

        // walk straight for the given length in that direction
        for(int i=0;i<corridorLength; i++)
        {
            currentPosition += direction;
            corridorPositions.Add(currentPosition);
        }

        return corridorPositions;
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
        {
            new Vector2Int(0,1), //UP
            new Vector2Int(0,-1), //DOWN
            new Vector2Int(1,0), //RIGHT
            new Vector2Int(-1,0) //LEFT
        };

        public static Vector2Int GetRandomDirection()
        {
            
            int randomIndex = UnityEngine.Random.Range(0, cardinalDirections.Count);

            return cardinalDirections[randomIndex];

        }
    }

    
}
