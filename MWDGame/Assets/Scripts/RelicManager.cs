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
            Destroy(gameObject); // 防止重复单例
        }
        else
        {
            Instance = this;
        }
    }

    public void TryGenerateRelic(Vector3 position)
    {
        // 1. 是否生成遗物
        if (Random.value > generateProbability)
        {
            Debug.Log("未触发生成遗物。");
            return;
        }

        // 2. 计算总权重
        float total = 0f;
        foreach (float prob in relicProbability)
        {
            total += prob;
        }

        if (total <= 0f)
        {
            Debug.LogWarning("概率权重总和为0，无法生成遗物。");
            return;
        }

        // 3. 加权随机生成遗物
        float rand = Random.Range(0f, total);
        float cumulative = 0f;

        for (int i = 0; i < ResourceManager.Instance.relicList.Count; i++)
        {
            cumulative += relicProbability[i];
            if (rand <= cumulative)
            {
                Instantiate(relicPrefab, position, Quaternion.identity);
                relicPrefab.GetComponent<RelicMono>().relicId = ResourceManager.Instance.relicList[i].relicId;
                Debug.Log($"生成了遗物：{relicPrefab.name}");
                return;
            }
        }
    }
}