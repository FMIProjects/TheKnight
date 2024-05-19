using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProceduralGenerationAlgorithms;

public static class WallGenerator 
{
    public static void  CreateWalls(HashSet<MapCell2> floorPositions,TilemapVisualizer tilemapVisualizer)
    {   
        // find the wall positions
        var wallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsCell2);

        // and then paint them
        tilemapVisualizer.PaintWallCells(wallPositions);
        
    }

    private static HashSet<MapCell2> FindWallsInDirections(HashSet<MapCell2> floorPositions, List<Vector2Int> directionList)
    {
        
        var wallPositions = new HashSet<MapCell2>();

        // for all floor positions verify if the neighbour is also painted
        foreach (var floorPosition in floorPositions)
        {

            foreach(var direction in directionList)
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
