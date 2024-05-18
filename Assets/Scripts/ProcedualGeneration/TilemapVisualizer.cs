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
        tilemap.SetTile(tilePosition, SelectRandomTile());
    }

    private TileBase SelectRandomTile()
    {
        return tiles[UnityEngine.Random.Range(0, tiles.Length)];
    }
}
