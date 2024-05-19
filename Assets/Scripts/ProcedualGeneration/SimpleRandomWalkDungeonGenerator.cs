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
        RunProceduralGeneration();
    }

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> positions = RunRandomWalk(walkParameters);

        // paint the floor tiles
        tilemapVisualizer.PaintFloorTiles(positions);
        // create the walls based on the floor positions
        WallGenerator.CreateWalls(positions, tilemapVisualizer);
        
    }

    // will pe used in inherited classes
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters)
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

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
