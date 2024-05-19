using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static ProceduralGenerationAlgorithms;

public static class WallGenerator 
{
    public static HashSet<MapCell2> CreateWalls(HashSet<MapCell2> floorPositions,TilemapVisualizer tilemapVisualizer)
    {   
        // find the wall positions
        var wallPositions = FindWallsInDirections(floorPositions);

        // and then paint them
        tilemapVisualizer.PaintWallCells(wallPositions);

        return wallPositions;
        
    }

    private static HashSet<MapCell2> GetIncompletedCorners(HashSet<MapCell2> floorPositions, HashSet<MapCell2> wallPositions)
    {
        var wallCorners = new HashSet<MapCell2>();

        return wallCorners;
    }

    private static HashSet<MapCell2> FindWallsInDirections(HashSet<MapCell2> floorPositions)
    {
        // get a list with all directions for a MapCell2
        List<Vector2Int> directionList = Direction2D.cardinalDirectionsCell2;
        List<Vector2Int> diagonalDirectionList = Direction2D.diagonalDirectionsCell2;

        List<Vector2Int> allDirections = new List<Vector2Int>(directionList);
        allDirections.AddRange(diagonalDirectionList);


        var wallPositions = new HashSet<MapCell2>();

        // for all floor positions verify if the neighbour is also painted
        foreach (var floorPosition in floorPositions)
        {
            foreach(var direction in allDirections)
            {
                var neighbour = floorPosition + direction;

                // if the neighbour is not a floor position, then it is a wall position
                if(!floorPositions.Contains(neighbour))
                {
                    wallPositions.Add(neighbour);
                }
            }

        }


        return wallPositions;
    }
}
