using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilemapVisualizerSO", menuName = "ProceduralGenerationSO/TilemapVisualizerSO")]
public class TilemapVisualizerSO : ScriptableObject
{
    public TileBase[] groundTiles;

    // the wall are 2x2 trees so we need to have 4 tiles for each corner
    public TileBase wallTileTopLeftCorner;
    public TileBase wallTileTopRightCorner;
    public TileBase wallTileBottomLeftCorner;
    public TileBase wallTileBottomRightCorner;

    // the wall ground tile which in the default exemople will be a grass tile
    public TileBase wallGroundTile;

    public int[] weights;
    public bool weightedSelection = false;
}
