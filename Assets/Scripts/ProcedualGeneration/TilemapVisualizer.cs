using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase[] tiles;
    [SerializeField]
    private int[] weights;
    [SerializeField]
    private bool weightedSelection = false;

    public void PaintFloorTiles(IEnumerable<Vector2Int> positions)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position);
        }
    }

    private void PaintSingleTile(Vector2Int position)
    {
        var tilePosition = new Vector3Int(position.x, position.y, 0);

        if(weightedSelection)
            tilemap.SetTile(tilePosition, SelectRandomWeightedTile());

        else
            tilemap.SetTile(tilePosition, SelectRandomTile());
        
    }

    private TileBase SelectRandomWeightedTile()
    {
        /*
         Implementation of weighted random selection using the roulette wheel selection algorithm
        */

        // get the partial sums of the weights and the total weight
        int numberTiles = tiles.Length;
        int[] partialSums = new int[numberTiles+1];
        partialSums[0] = 0;

        for(int i=0;i<numberTiles;++i)
        {
            partialSums[i+1] = partialSums[i] + weights[i];
        }

        int totalWeight = partialSums[numberTiles];
        
        // generate a random weight and find the corresponding tile
        int randomWeight = UnityEngine.Random.Range(1, totalWeight+1);

        // binary search to find the index
        int randomIndex = Array.BinarySearch(partialSums, randomWeight);

        //Debug.Log($"Random Weight: {randomWeight}, Random Index: {randomIndex}");

        /*
         If the weight is found return the index
         If the value is not found , the method returns a negative number that is the bitwise complement of the index of the next element that is larger than the value
         */
        return tiles[randomIndex >= 0 ? randomIndex-1 : ~randomIndex-1];
    }

    private TileBase SelectRandomTile()
    {
        return tiles[UnityEngine.Random.Range(0, tiles.Length)];
    }
}
