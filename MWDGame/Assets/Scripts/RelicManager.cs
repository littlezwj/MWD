using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public static RelicManager Instance { get; private set; }

    public GameObject relicPrefab;
    public List<float> relicProbability;
    public float generateProbability = 0.3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ��ֹ�ظ�����
        }
        else
        {
            Instance = this;
        }
    }

    public void TryGenerateRelic(Vector3 position)
    {
        // 1. �Ƿ���������
        if (Random.value > generateProbability)
        {
            Debug.Log("δ�����������");
            return;
        }

        // 2. ������Ȩ��
        float total = 0f;
        foreach (float prob in relicProbability)
        {
            total += prob;
        }

        if (total <= 0f)
        {
            Debug.LogWarning("����Ȩ���ܺ�Ϊ0���޷��������");
            return;
        }

        // 3. ��Ȩ�����������
        float rand = Random.Range(0f, total);
        float cumulative = 0f;

        for (int i = 0; i < ResourceManager.Instance.relicList.Count; i++)
        {
            cumulative += relicProbability[i];
            if (rand <= cumulative)
            {
                Instantiate(relicPrefab, position, Quaternion.identity);
                relicPrefab.GetComponent<RelicMono>().relicId = ResourceManager.Instance.relicList[i].relicId;
                Debug.Log($"���������{relicPrefab.name}");
                return;
            }
        }
    }
}