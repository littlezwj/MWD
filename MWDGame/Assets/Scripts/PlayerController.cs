// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public enum ControlScheme { WASD, ArrowKeys }

//     [Header("控制设置")]
//     public ControlScheme controlScheme = ControlScheme.WASD;
//     public float moveSpeed = 5f;
//     public GameObject shovelPrefab;
//     public LayerMask obstacleLayer;
//     public Grid grid;

//     [Header("道具逻辑")]
//     public PlayerInventory playerInventory; 

//     private Rigidbody2D rb;
//     private bool isMoving = false;
//     private Vector2 moveDirection;
//     private Vector3 targetPos;

//     void Start()
//     {
//         SnapToGrid();
//         rb = GetComponent<Rigidbody2D>();
//     }

//     void Update()
//     {
//         if (!isMoving)
//         {
//             Vector2 input = GetInput();
//             if (input != Vector2.zero)
//             {
//                 moveDirection = input;
//                 if (CanMoveToNextTile(moveDirection))
//                 {
//                     StartCoroutine(MoveToNextTile(moveDirection));
//                 }
//             }
//             else
//             {
//                 HandleItemOrShovel();
//             }
//         }
//     }

//     private void HandleItemOrShovel()
//     {
//         if (controlScheme == ControlScheme.WASD)
//         {
//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 UseItemOrShovel();
//             }
//         }
//         else if (controlScheme == ControlScheme.ArrowKeys)
//         {
//             if (Input.GetKeyDown(KeyCode.Return))
//             {
//                 UseItemOrShovel();
//             }
//         }
//     }

//     private void UseItemOrShovel()
//     {
//         if (playerInventory.isEquipped)
//         {
//             UseRelicById(playerInventory.relicId);
//             playerInventory.isEquipped = false;
//             playerInventory.relicId = 0;
//         }
//         else
//         {
//             PlaceShovel();
//         }
//     }

//     private void UseRelicById(int id)
//     {
//         switch (id)
//         {
//             case 1:
//                 Debug.Log("使用道具 1：加速");
//                 StartCoroutine(SpeedBoost());
//                 break;
//             case 2:
//                 Debug.Log("使用道具 2：清除周围障碍");
//                 ClearNearbyObstacles();
//                 break;
//             default:
//                 Debug.Log("未知道具ID：" + id);
//                 break;
//         }
//     }

//     private IEnumerator SpeedBoost()
//     {
//         moveSpeed *= 2;
//         yield return new WaitForSeconds(5f);
//         moveSpeed /= 2;
//     }

//     private void ClearNearbyObstacles()
//     {
//         Vector3Int centerCell = grid.WorldToCell(transform.position);
//         Vector3 worldPos = grid.GetCellCenterWorld(centerCell);
//         Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 1f);

//         foreach (var hit in hits)
//         {
//             if (hit.CompareTag("BlockA"))
//             {
//                 Destroy(hit.gameObject);
//             }
//         }
//     }

//     private Vector2 GetInput()
//     {
//         float h = 0f, v = 0f;

//         if (controlScheme == ControlScheme.WASD)
//         {
//             if (Input.GetKey(KeyCode.W)) v += 1f;
//             if (Input.GetKey(KeyCode.S)) v -= 1f;
//             if (Input.GetKey(KeyCode.A)) h -= 1f;
//             if (Input.GetKey(KeyCode.D)) h += 1f;
//         }
//         else if (controlScheme == ControlScheme.ArrowKeys)
//         {
//             if (Input.GetKey(KeyCode.UpArrow)) v += 1f;
//             if (Input.GetKey(KeyCode.DownArrow)) v -= 1f;
//             if (Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
//             if (Input.GetKey(KeyCode.RightArrow)) h += 1f;
//         }

//         if (h != 0) v = 0;  // 禁止斜向移动
//         return new Vector2(h, v);
//     }

//     private IEnumerator MoveToNextTile(Vector2 direction)
//     {
//         isMoving = true;

//         Vector3Int currentCell = grid.WorldToCell(transform.position);
//         Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
//         targetPos = grid.GetCellCenterWorld(targetCell);

//         while ((targetPos - transform.position).sqrMagnitude > 0.01f)
//         {
//             transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
//             yield return null;
//         }

//         transform.position = targetPos;
//         isMoving = false;

//         Vector2 input = GetInput();
//         if (input != Vector2.zero && CanMoveToNextTile(input))
//         {
//             moveDirection = input;
//             StartCoroutine(MoveToNextTile(moveDirection));
//         }
//     }

//     private void SnapToGrid()
//     {
//         Vector3Int cellPos = grid.WorldToCell(transform.position);
//         transform.position = grid.GetCellCenterWorld(cellPos);
//     }

//     private void PlaceShovel()
//     {
//         Vector3Int cellPos = grid.WorldToCell(transform.position); // 重新计算当前位置所在格子
//         Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);

//         GameObject shovel = Instantiate(shovelPrefab, spawnPos, Quaternion.identity);

//         // 给新实例的ShovelController赋值grid
//         ShovelController shovelCtrl = shovel.GetComponent<ShovelController>();
//         if (shovelCtrl != null)
//         {
//             shovelCtrl.grid = grid; // 传入当前PlayerController的grid引用
//         }
//     }

//     private bool CanMoveToNextTile(Vector2 direction)
//     {
//         Vector3Int currentCell = grid.WorldToCell(transform.position);
//         Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
//         Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);

//         Collider2D hitObstacle = Physics2D.OverlapCircle(targetWorldPos, 0.1f, obstacleLayer);
//         if (hitObstacle != null) return false;

//         Collider2D[] hits = Physics2D.OverlapCircleAll(targetWorldPos, 0.1f);
//         foreach (var hit in hits)
//         {
//             if (hit.CompareTag("Player") && hit.gameObject != this.gameObject)
//             {
//                 return false;
//             }
//         }

//         return true;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlScheme { WASD, ArrowKeys }

    [Header("控制设置")]
    public ControlScheme controlScheme = ControlScheme.WASD;
    public float moveSpeed = 5f;
    public Grid grid;
    public LayerMask obstacleLayer;

    [Header("组件引用")]
    public PlayerInventory playerInventory;
    public ShovelPlacer shovelPlacer;

    private Rigidbody2D rb;
    private bool isMoving = false;
    private Vector2 moveDirection;
    private Vector3 targetPos;

    void Start()
    {
        SnapToGrid();
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.up; // 初始默认朝上
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = GetInput();

            if (input != Vector2.zero)
            {
                moveDirection = input;

                if (CanMoveToNextTile(moveDirection))
                {
                    StartCoroutine(MoveToNextTile(moveDirection));
                }
            }
            else
            {
                HandleInput();
            }
        }
    }

    private void HandleInput()
    {
        if (controlScheme == ControlScheme.WASD)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ClearBlockInFront();

            if (Input.GetKeyDown(KeyCode.E))
                shovelPlacer.PlaceShovel(transform.position);
        }
        else if (controlScheme == ControlScheme.ArrowKeys)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                ClearBlockInFront();

            if (Input.GetKeyDown(KeyCode.Comma))
                shovelPlacer.PlaceShovel(transform.position);
        }
    }

    private void ClearBlockInFront()
    {
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int((int)moveDirection.x, (int)moveDirection.y, 0);
        Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);

        Collider2D[] hits = Physics2D.OverlapCircleAll(targetWorldPos, 0.3f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("BlockA"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    private Vector2 GetInput()
    {
        float h = 0f, v = 0f;

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

        if (h != 0) v = 0;
        return new Vector2(h, v);
    }

    private IEnumerator MoveToNextTile(Vector2 direction)
    {
        isMoving = true;

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

        Vector2 input = GetInput();
        if (input != Vector2.zero && CanMoveToNextTile(input))
        {
            moveDirection = input;
            StartCoroutine(MoveToNextTile(moveDirection));
        }
    }

    private void SnapToGrid()
    {
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPos);
    }

    private bool CanMoveToNextTile(Vector2 direction)
    {
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
        Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);

        Collider2D hitObstacle = Physics2D.OverlapCircle(targetWorldPos, 0.1f, obstacleLayer);
        if (hitObstacle != null) return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(targetWorldPos, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && hit.gameObject != this.gameObject)
            {
                return false;
            }
        }

        return true;
    }
}
