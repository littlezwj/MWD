using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{   
    public static SkillManager Instance { get; private set; }
    public PlayerController[] players = new PlayerController[2];

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
            default:
                Debug.LogError("没有找到对应编号的道具技能！");
                break;
        }
        players[player].playerInventory.isEquipped = false;
        players[player].playerInventory.relicId = 0;
    }
}
