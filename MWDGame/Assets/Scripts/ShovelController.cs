using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public float delayTime = 1f;
    private Grid grid;
    private GridManager gridManager;

    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        StartCoroutine(ShovelDelay());
    }

    IEnumerator ShovelDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Dig();
        Destroy(gameObject);
    }

    void Dig()
    {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector2Int center = new Vector2Int(cellPos.x, cellPos.y);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int pos = center + new Vector2Int(x, y);
                if (gridManager.gridMap.ContainsKey(pos) && gridManager.gridMap[pos] == BlockType.BlockA)
                {
                    gridManager.gridMap[pos] = BlockType.Empty;
                    gridManager.tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
                    // TODO：掉落道具
                }
            }
        }
    }
}
