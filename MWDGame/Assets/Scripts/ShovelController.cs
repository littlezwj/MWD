using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public Grid grid; // 用于坐标转换
    public float blinkDuration = 0.3f; // 一次闪烁时间
    public int blinkCount = 3;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (grid == null)
        {
            Debug.LogError("Grid is not assigned in ShovelController.");
            return;
        }

        StartCoroutine(BlinkAndDestroy());
    }

    IEnumerator BlinkAndDestroy()
    {
        // 闪烁3次
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }

        // 消除BlockA
        DestroyNearbyBlockA();

        // 自身销毁
        Destroy(gameObject);
    }

    private void DestroyNearbyBlockA()
    {
        Vector3Int centerCell = grid.WorldToCell(transform.position);
        Vector3Int[] directions = new Vector3Int[]
        {
            centerCell,
            centerCell + new Vector3Int(0, 1, 0),
            centerCell + new Vector3Int(0, -1, 0),
            centerCell + new Vector3Int(1, 0, 0),
            centerCell + new Vector3Int(-1, 0, 0),
        };

        float radius = 0.3f;

        foreach (var cell in directions)
        {
            Vector3 worldPos = grid.GetCellCenterWorld(cell);
            Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, radius);

            foreach (var hit in hits)
            {
                // 清除 BlockA
                if (hit.CompareTag("BlockA"))
                {
                    Destroy(hit.gameObject);
                    RelicManager.Instance.TryGenerateRelic(hit.gameObject.transform.position);
                }

                // 无论是谁，只要是玩家就扣血
                Health health = hit.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(1); // 直接扣血
                }
            }
        }
    }

}
