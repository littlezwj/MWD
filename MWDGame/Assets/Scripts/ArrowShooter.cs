using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooter : MonoBehaviour
{
    public LayerMask hitTargets;      // 可检测的目标层
    public LayerMask blockingLayer;   // 会阻挡射线的层（如墙壁）
    public Grid mapGrid;
    public GameObject arrow;
    public float arrowSpeed = 5.0f;
    public float waitingTime = 3.0f;
    private bool activated = false;
    public float rayLength = 5f;
    private void Start()
    {
        mapGrid = GameObject.Find("Grid").GetComponent<Grid>();
        hitTargets = LayerMask.GetMask("PlayerLayer");
        blockingLayer = LayerMask.GetMask("obstacleLayer");
        Invoke("Activate", waitingTime);
    }

    private void Update()
    {
        if(activated)
        {
            CastRaysInFourDirections();
        }      
    }
    void CastRaysInFourDirections()
    {
        Vector2 origin = transform.position;

        // 四个方向
        Vector2[] directions = new Vector2[]
        {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
        };

        foreach (Vector2 dir in directions)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, rayLength);

            Debug.DrawRay(origin, dir * rayLength * mapGrid.cellSize.x, Color.red, 0.5f); // 可视化射线

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == null) continue;

                // 如果是阻挡物，停止射线
                if (((1 << hit.collider.gameObject.layer) & blockingLayer) != 0)
                {
                    Debug.Log("[" + dir + "] 射线被阻挡，停止在：" + hit.collider.name);
                    break;
                }

                // 如果是可击中的目标
                if (((1 << hit.collider.gameObject.layer) & hitTargets) != 0)
                {
                    Debug.Log("[" + dir + "] 可命中目标：" + hit.collider.name);
                    GameObject arrowGO = Instantiate(arrow, transform.position + new Vector3(dir.x * mapGrid.cellSize.x, dir.y * mapGrid.cellSize.y, 0), Quaternion.identity);
                    arrowGO.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * arrowSpeed, dir.y * arrowSpeed);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void Activate()
    {
        activated = true;
    }
}
