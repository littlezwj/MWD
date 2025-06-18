using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{   
    public static SkillManager Instance { get; private set; }
    public PlayerController[] players = new PlayerController[2];
    public GameObject arrow;
    public GameObject arrowShooter;
    public float arrowSpeed = 2.5f;
    public Grid mapGrid;

    private void Awake()
    {
        // 实现单例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 避免重复单例
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RelicSkillTable(int relicId, int player)
    {
        switch(relicId)
        {
            case 101:
                break;
            //帛画《导引图》
            case 102:
                players[player].moveSpeed *= 1.5f;
                Debug.Log("加速了一次");
                break;
            //“非衣”帛画
            case 103:
                players[player].GetComponent<Health>().holdingFeiyi = true;
                break;
            //云龙纹漆屏风
            case 104:
                GameObject waitingShooter = Instantiate(arrowShooter, players[player].transform.position, Quaternion.identity);
                break;
            //锥画漆弩机
            case 107:
                PlayerController pc = players[player].GetComponent<PlayerController>();
                GameObject arrowShoted = Instantiate(arrow, players[player].transform.position + new Vector3(pc.moveDirection.x * mapGrid.cellSize.x , pc.moveDirection.y * mapGrid.cellSize.x , 0), Quaternion.identity);
                arrowShoted.GetComponent<Rigidbody2D>().velocity = new Vector2((int)pc.moveDirection.x * arrowSpeed, (int)pc.moveDirection.y * arrowSpeed);
                break;
            //"冠人"男俑
            case 108:
                players[player].GetComponent<Health>().currentHealth++;
                players[player].GetComponent<Health>().UpdateHealthUI();
                break;
            //帛画《车马仪仗图》
            case 109:
                players[player].GetComponent<PlayerController>().ghost = true;
                Collider2D playerColl = players[player].GetComponent<Collider2D>();
                string targetLayerName = "obstacleLayer";
                float ignoreDuration = 60f;
                StartCoroutine(IgnoreCollisionCoroutine(targetLayerName, playerColl, ignoreDuration));
                break;
            default:
                Debug.LogError("没有找到对应编号的道具技能！");
                break;
        }
        players[player].playerInventory.ResetInvetory();
    }

    private IEnumerator IgnoreCollisionCoroutine(string targetLayerName, Collider2D myCollider, float ignoreDuration)
    {
        // 找出目标层所有Collider
        int targetLayer = LayerMask.NameToLayer(targetLayerName);
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        List<Collider2D> ignoredColliders = new List<Collider2D>();
        ignoredColliders.Clear();
        foreach (var col in allColliders)
        {
            if (col.gameObject.layer == targetLayer)
            {
                Debug.Log("存在一个目标层级建筑");
                Physics2D.IgnoreCollision(myCollider, col, true);
                ignoredColliders.Add(col);
            }
        }

        yield return new WaitForSeconds(ignoreDuration);

        // 恢复碰撞
        foreach (var col in ignoredColliders)
        {
            if (col != null)
                Physics2D.IgnoreCollision(myCollider, col, false);
        }

        ignoredColliders.Clear();
    }
}
