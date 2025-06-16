using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public List<RelicDetails> relicList;

    private void Awake()
    {
        // ������ʼ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ����Ѿ���ʵ�����������ظ���
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ��ѡ���г���ʱ�����ö���
    }

    public RelicDetails GetRelicById(int id)
    {
        return relicList.FirstOrDefault(r => r.relicId == id);
    }
}
