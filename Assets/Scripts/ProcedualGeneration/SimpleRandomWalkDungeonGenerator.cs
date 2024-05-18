using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : DungeonGenerator
{
    [SerializeField]
    SimpleRandomWalkSO parameters;


    private void Start()
    {
        RunProceduralGeneration();
    }

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> positions = RunRandomWalk();

        tilemapVisualizer.PaintFloorTiles(positions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
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
