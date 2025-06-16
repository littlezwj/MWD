using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlScheme
    {
        WASD,
        ArrowKeys
    }

    [Header("控制设置")]
    public ControlScheme controlScheme = ControlScheme.WASD;
    public float moveSpeed = 5f;
    public GameObject shovelPrefab;

    public LayerMask obstacleLayer; // 包括：墙体、方块B、其他玩家

    [Header("网格设置")]
    public Grid grid;  // 直接拖拽你的Tilemap Grid进来

    private bool isMoving = false;
    private Vector2 moveDirection;
    private Vector3 targetPos;

    void Start()
    {
        SnapToGrid();
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = GetInput();

            if (input != Vector2.zero)
            {
                moveDirection = input;

                // 检查目标格子是否被占用
                if (CanMoveToNextTile(moveDirection))
                {
                    StartCoroutine(MoveToNextTile(moveDirection));
                }
            }
            else
            {
                HandlePlaceShovel();
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) MoveToNextTile(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.S)) MoveToNextTile(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.A)) MoveToNextTile(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.D)) MoveToNextTile(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.Space)) PlaceShovel();
    }

    /// 读取键盘输入
    private Vector2 GetInput()
    {
        float h = 0f;
        float v = 0f;

        if (controlScheme == ControlScheme.WASD)
        {
            if (Input.GetKey(KeyCode.W)) v += 1f;
            if (Input.GetKey(KeyCode.S)) v -= 1f;
            if (Input.GetKey(KeyCode.A)) h -= 1f;
            if (Input.GetKey(KeyCode.D)) h += 1f;
        }
        else if (controlScheme == ControlScheme.ArrowKeys)
        {
            if (Input.GetKey(KeyCode.UpArrow)) v += 1f;
            if (Input.GetKey(KeyCode.DownArrow)) v -= 1f;
            if (Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) h += 1f;
        }

        // 只允许一个方向移动（禁止斜着移动）
        if (h != 0)
            v = 0;

        return new Vector2(h, v);
    }

    /// 执行平滑移动
    IEnumerator MoveToNextTile(Vector2 direction)
    {
        isMoving = true;

        // 计算目标格子中心位置
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
        targetPos = grid.GetCellCenterWorld(targetCell);

        while ((targetPos - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        // 移动完自动检测是否继续移动（保持按键连贯移动）
        Vector2 input = GetInput();
        if (input != Vector2.zero && CanMoveToNextTile(input))
        {
            moveDirection = input;
            StartCoroutine(MoveToNextTile(moveDirection));
        }
    }

    /// 瞬间对齐到格子中心
    private void SnapToGrid()
    {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPos);
    }

    /// 放置铲子逻辑
    private void HandlePlaceShovel()
    {
        if (controlScheme == ControlScheme.WASD)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaceShovel();
            }
        }
        else if (controlScheme == ControlScheme.ArrowKeys)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlaceShovel();
            }
        }
    }

    private void PlaceShovel()
    {
        Vector3Int cellPos = new Vector3Int(currentCell.x, currentCell.y, 0);
        Vector3 spawnPos = grid.CellToWorld(cellPos) + grid.cellSize / 2;
        Instantiate(shovelPrefab, spawnPos, Quaternion.identity);
    }

    /// 检查目标格子是否能走
    private bool CanMoveToNextTile(Vector2 direction)
{
    Vector3Int currentCell = grid.WorldToCell(transform.position);
    Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
    Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);

    // 1. 先检测环境障碍（不可消除方块B、墙体）
    Collider2D hitObstacle = Physics2D.OverlapCircle(targetWorldPos, 0.1f, obstacleLayer);
    if (hitObstacle != null)
    {
        return false;
    }

    // 2. 再检测是否有其他玩家站在目标格子
    Collider2D[] hits = Physics2D.OverlapCircleAll(targetWorldPos, 0.1f);
    foreach (var hit in hits)
    {
        if (hit.CompareTag("Player") && hit.gameObject != this.gameObject)
        {
            return false; // 有其他玩家在此格子
        }
    }

    return true;
}
}
