using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// a cell will be a 2x2 square
public class MapCell2
{

    public Vector2Int topLeftCorner;
    public Vector2Int bottomLeftCorner;
    public Vector2Int topRightCorner;
    public Vector2Int bottomRightCorner;
    
    // method used to flatten the cell to a list of positions   
    public HashSet<Vector2Int> getCorners()
    {
        return new HashSet<Vector2Int>
        {
            topLeftCorner,
            bottomLeftCorner,
            topRightCorner,
            bottomRightCorner
        };
    }   

    // the zero cell
    public static MapCell2 zero = CreateCell(Vector2Int.zero);

    // factory method to create a cell
    public static MapCell2 CreateCell(Vector2Int startPosition)
    {
        MapCell2 cell = new MapCell2();

        // create a cell which will be on the right of the start startPosition

        cell.topLeftCorner = new Vector2Int(startPosition.x,startPosition.y);
        cell.bottomLeftCorner = new Vector2Int(startPosition.x, startPosition.y-1);
        cell.topRightCorner = new Vector2Int(startPosition.x+1, startPosition.y);
        cell.bottomRightCorner = new Vector2Int(startPosition.x + 1, startPosition.y-1);

        return cell;
    }
    
    public static Vector2 ComputeMiddle(MapCell2 cell)
    {
        return new Vector2((cell.topLeftCorner.x + cell.topRightCorner.x) / 2.0f, (cell.topLeftCorner.y + cell.bottomLeftCorner.y) / 2.0f);
    }

    public static float Distance(MapCell2 cell1, MapCell2 cell2)
    {
        var middle1 = ComputeMiddle(cell1);
        var middle2 = ComputeMiddle(cell2);

        return Vector2.Distance(middle1, middle2);
    }   

    // operator overloads

    public static MapCell2 operator +(MapCell2 cell , Vector2Int offset)
    {
        MapCell2 newCell = new MapCell2();
        newCell.topLeftCorner = cell.topLeftCorner + offset;
        newCell.bottomLeftCorner = cell.bottomLeftCorner + offset;
        newCell.topRightCorner = cell.topRightCorner + offset;
        newCell.bottomRightCorner = cell.bottomRightCorner + offset;
        return newCell;

    }

    public static MapCell2 operator -(MapCell2 cell, Vector2Int offset)
    {
        MapCell2 newCell = new MapCell2();
        newCell.topLeftCorner = cell.topLeftCorner - offset;
        newCell.bottomLeftCorner = cell.bottomLeftCorner - offset;
        newCell.topRightCorner = cell.topRightCorner - offset;
        newCell.bottomRightCorner = cell.bottomRightCorner - offset;
        return newCell;
    }


    // These are needed for HashSet

    // Override Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        MapCell2 other = (MapCell2)obj;
        return topLeftCorner == other.topLeftCorner &&
               bottomLeftCorner == other.bottomLeftCorner &&
               topRightCorner == other.topRightCorner &&
               bottomRightCorner == other.bottomRightCorner;
    }

    // Override GetHashCode
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            hash = hash * 23 + topLeftCorner.GetHashCode();
            hash = hash * 23 + bottomLeftCorner.GetHashCode();
            hash = hash * 23 + topRightCorner.GetHashCode();
            hash = hash * 23 + bottomRightCorner.GetHashCode();
            return hash;
        }
    }

    //Override ToString 
    public override string ToString()
    {
        return $"TopLeft: {topLeftCorner}, BottomLeft: {bottomLeftCorner}, TopRight: {topRightCorner}, BottomRight: {bottomRightCorner}";
    }


}
