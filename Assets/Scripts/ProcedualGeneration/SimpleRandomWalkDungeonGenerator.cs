using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomly = true;

    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    private void Start()
    {
        RunProceduralGeneration();
    }

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> positions = RunRandomWalk();

        tilemapVisualizer.PaintFloorTiles(positions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        // start the simple walk for a number of iterations from a random position
        for(int i=0; i < iterations; ++i)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            positions.UnionWith(path);

            if(startRandomly)
                currentPosition = positions.ElementAt(Random.Range(0,positions.Count));
            
        }

        return positions;
    }

}
