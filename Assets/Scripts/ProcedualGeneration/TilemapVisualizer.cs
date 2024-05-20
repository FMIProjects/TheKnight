using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    
    private Tilemap tilemapGround;
    private Tilemap tilemapCollider;

    [SerializeField]
    private TilemapVisualizerSO parameters;

    private void Awake()
    {
        // get the tilemmaps from the scene
        tilemapCollider = GameObject.FindWithTag("TileMapCollider").GetComponent<Tilemap>();
        tilemapGround = GameObject.FindWithTag("TileMapGround").GetComponent<Tilemap>();

        // validate the weights if the selection is weighted
        if (parameters.weightedSelection)
        {
            ValidateWeights();
        }

    }

    // the walls will be trees which have a size of 2x2
    // so the parameter will pe an enumerable of MapCell2

    public void PaintWallCells(IEnumerable<MapCell2> positions)
    {
        foreach (var position in positions)
        {
            PaintSingleWallCell(position);
        }
    }

    private void PaintSingleWallCell(MapCell2 position)
    {
        // get the 3d position of the tiles
        var tilePositionTopLeftCorner = new Vector3Int(position.topLeftCorner.x, position.topLeftCorner.y, 0);
        var tilePositionTopRightCorner = new Vector3Int(position.topRightCorner.x, position.topRightCorner.y, 0);
        var tilePositionBottomLeftCorner = new Vector3Int(position.bottomLeftCorner.x, position.bottomLeftCorner.y, 0);
        var tilePositionBottomRightCorner = new Vector3Int(position.bottomRightCorner.x, position.bottomRightCorner.y, 0);

        // set the ground tile of the wall
        tilemapGround.SetTile(tilePositionTopLeftCorner, parameters.wallGroundTile);
        tilemapGround.SetTile(tilePositionTopRightCorner, parameters.wallGroundTile); 
        tilemapGround.SetTile(tilePositionBottomLeftCorner, parameters.wallGroundTile);
        tilemapGround.SetTile(tilePositionBottomRightCorner, parameters.wallGroundTile);

        // set the wall tiles
        tilemapCollider.SetTile(tilePositionTopLeftCorner, parameters.wallTileTopLeftCorner);
        tilemapCollider.SetTile(tilePositionTopRightCorner, parameters.wallTileTopRightCorner);
        tilemapCollider.SetTile(tilePositionBottomLeftCorner, parameters.wallTileBottomLeftCorner);
        tilemapCollider.SetTile(tilePositionBottomRightCorner, parameters.wallTileBottomRightCorner);

        
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> positions)
    {
        
        foreach (var position in positions)
        {
            PaintSingleFloorTile(position);
        }
    }

    public void PaintSingleFloorTile(Vector2Int position)
    {
        
        var tilePosition = new Vector3Int(position.x, position.y, 0);

        if(parameters.weightedSelection)
            tilemapGround.SetTile(tilePosition, SelectRandomWeightedTile());

        else
            tilemapGround.SetTile(tilePosition, SelectRandomTile());
        
    }

    private TileBase SelectRandomWeightedTile()
    {
        /*
         Implementation of weighted random selection using the roulette wheel selection algorithm
        */

        // get the partial sums of the weights and the total weight
        int numberTiles = parameters.groundTiles.Length;
        int[] partialSums = new int[numberTiles+1];
        partialSums[0] = 0;

        for(int i=0;i<numberTiles;++i)
        {
            partialSums[i+1] = partialSums[i] + parameters.weights[i];
        }

        int totalWeight = partialSums[numberTiles];
        
        // generate a random weight and find the corresponding tile
        int randomWeight = UnityEngine.Random.Range(1, totalWeight+1);

        // binary search to find the index
        int randomIndex = Array.BinarySearch(partialSums, randomWeight);

        /*
         If the weight is found return the index
         If the value is not found , the method returns a negative number that is the bitwise complement of the index of the next element that is larger than the value
         */
        return parameters.groundTiles[randomIndex >= 0 ? randomIndex-1 : ~randomIndex-1];
    }

    private TileBase SelectRandomTile()
    {
        return parameters.groundTiles[UnityEngine.Random.Range(0, parameters.groundTiles.Length)];
    }

    private void ValidateWeights()
    {
        int numberTiles = parameters.groundTiles.Length;
        int numberWeights = parameters.weights.Length;

        Debug.Assert(numberTiles == numberWeights, "The number of weights should be at least the number of tiles.");
    }

}
