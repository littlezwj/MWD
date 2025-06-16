using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelPlacer : MonoBehaviour
{
    public GameObject shovelPrefab;
    public Grid grid;

    public void PlaceShovel(Vector3 position)
    {
        Vector3Int cellPos = grid.WorldToCell(position);
        Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);
        GameObject shovel = Instantiate(shovelPrefab, spawnPos, Quaternion.identity);

        ShovelController shovelCtrl = shovel.GetComponent<ShovelController>();
        if (shovelCtrl != null)
        {
            shovelCtrl.grid = grid;
        }
    }
}
