using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    

    public static HashSet<MapCell2> SimpleRandomWalk(MapCell2 startPosition, int steps)
    {
        HashSet<MapCell2> path = new HashSet<MapCell2>();
        path.Add(startPosition);

        var previousPosition = startPosition;

        for (int i = 0; i < steps; i++)
        {
            // generate take a step in a random direction
            var newPosition = previousPosition + Direction2D.GetRandomDirectionCell2();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    public static List<MapCell2> RandomWalkCorridor(MapCell2 statPosition, int corridorLength)
    {
        // generate a corridor of a given length
        List<MapCell2> corridorPositions = new List<MapCell2>();
        // get a random direction
        var direction = Direction2D.GetRandomDirectionCell2();
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

        public static List<Vector2Int> cardinalDirectionsCell2 = new List<Vector2Int>
        {
            new Vector2Int(0,2), //UP
            new Vector2Int(0,-2), //DOWN
            new Vector2Int(2,0), //RIGHT
            new Vector2Int(-2,0) //LEFT
        };

        public static List<Vector2Int> diagonalDirectionsCell2 = new List<Vector2Int>
        {
            new Vector2Int(2,2), //UP-RIGHT
            new Vector2Int(2,-2), //DOWN-RIGHT
            new Vector2Int(-2,2), //UP-LEFT
            new Vector2Int(-2,-2) //DOWN-LEFT
        };

        public static Vector2Int GetRandomDirection()
        {
            
            int randomIndex = UnityEngine.Random.Range(0, cardinalDirections.Count);

            return cardinalDirections[randomIndex];

        }

        public static Vector2Int GetRandomDirectionCell2()
        {
            int randomIndex = UnityEngine.Random.Range(0, cardinalDirectionsCell2.Count);

            return cardinalDirectionsCell2[randomIndex];
        }

    }

    
}
