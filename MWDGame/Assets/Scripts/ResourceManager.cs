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
        // 单例初始化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 如果已经有实例，则销毁重复的
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 可选：切场景时保留该对象
    }

    public RelicDetails GetRelicById(int id)
    {
        return relicList.FirstOrDefault(r => r.relicId == id);
    }
}
