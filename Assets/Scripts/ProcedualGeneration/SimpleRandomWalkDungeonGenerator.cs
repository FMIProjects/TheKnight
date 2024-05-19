using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : DungeonGenerator
{
    [SerializeField]
    SimpleRandomWalkSO walkParameters;


    private void Start()
    {
        GenerateDungeon();
    }

    protected override void RunProceduralGeneration()
    {
        
        HashSet<MapCell2> positions = RunRandomWalk(walkParameters);
        
        IEnumerable<Vector2Int> vectorPositions = positions.SelectMany(cell => cell.getCorners());
        
        // paint the floor tiles
        tilemapVisualizer.PaintFloorTiles(vectorPositions);
        // create the walls based on the floor positions
        WallGenerator.CreateWalls(positions, tilemapVisualizer);
        
    }

    // will pe used in inherited classes
    protected HashSet<MapCell2> RunRandomWalk(SimpleRandomWalkSO parameters)
    {
        var currentPosition = startPosition;
        HashSet<MapCell2> positions = new HashSet<MapCell2>();

        // start the simple walk for a number of iterations from a random position
        for(int i=0; i < parameters.iterations; ++i)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            positions.UnionWith(path);

            // each time generate a new random start position
            if(parameters.startRandomly)
                currentPosition = positions.ElementAt(Random.Range(0,positions.Count));
            
        }

        return positions;
    }

}
