using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public float delayBeforeDig = 2f;
    public Tilemap tilemap;
    public TileBase tileA;
    public GameObject[] itemPrefabs;

    void Start()
    {
        StartCoroutine(DigAfterDelay());
    }

    IEnumerator DigAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDig);
        Dig();
        Destroy(gameObject);
    }

    void Dig()
    {
        Vector3Int centerCell = tilemap.WorldToCell(transform.position);
        Vector3Int[] directions = { Vector3Int.zero, Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        foreach (var dir in directions)
        {
            Vector3Int targetCell = centerCell + dir;
            TileBase targetTile = tilemap.GetTile(targetCell);
            if (targetTile == tileA)
            {
                tilemap.SetTile(targetCell, null);
                TrySpawnItem(targetCell);
            }
        }
    }

    void TrySpawnItem(Vector3Int cellPosition)
    {
        if (Random.value < 0.5f)
        {
            int randIndex = Random.Range(0, itemPrefabs.Length);
            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPosition);
            Instantiate(itemPrefabs[randIndex], worldPos, Quaternion.identity);
        }
    }
}
