using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{   
    public static SkillManager Instance { get; private set; }
    public PlayerController[] players = new PlayerController[2];

    private void Awake()
    {
        // ʵ�ֵ���
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �����ظ�����
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
            //����������ͼ��
            case 102:
                players[player].moveSpeed *= 1.5f;
                Debug.Log("������һ��");
                break;
            default:
                Debug.LogError("û���ҵ���Ӧ��ŵĵ��߼��ܣ�");
                break;
        }
        players[player].playerInventory.isEquipped = false;
        players[player].playerInventory.relicId = 0;
    }
}
