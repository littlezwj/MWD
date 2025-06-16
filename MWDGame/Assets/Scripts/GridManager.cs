using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileA;
    public TileBase tileB;

    public Dictionary<Vector2Int, BlockType> gridMap = new Dictionary<Vector2Int, BlockType>();

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile == tileA)
            {
                gridMap[new Vector2Int(pos.x, pos.y)] = BlockType.BlockA;
            }
            else if (tile == tileB)
            {
                gridMap[new Vector2Int(pos.x, pos.y)] = BlockType.BlockB;
            }
        }
    }
}

public enum BlockType
{
    Empty,
    BlockA,
    BlockB
}