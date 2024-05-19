using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilemapVisualizerSO", menuName = "ProceduralGenerationSO/TilemapVisualizerSO")]
public class TilemapVisualizerSO : ScriptableObject
{
    public TileBase[] groundTiles;
    public TileBase wallTile;
    public TileBase wallGroundTile;
    public int[] weights;
    public bool weightedSelection = false;
}
