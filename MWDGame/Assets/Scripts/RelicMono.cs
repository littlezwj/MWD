using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class RelicMono : MonoBehaviour
{
    public int relicId;
    public SpriteRenderer spriteRenderer;
    public Sprite iconOnPlayer;
    public Sprite iconOnMap;
    public float spawnTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ResourceManager.Instance != null)
        {
            Debug.Log("ResourceManager.Instance 已成功找到！");
        }
        else
        {
            Debug.LogError("ResourceManager.Instance 未找到！请确认 ResourceManager 脚本已挂载并正确设置单例。");
        }
        iconOnPlayer = ResourceManager.Instance.GetRelicById(relicId).iconOnPlayer;
        iconOnMap = ResourceManager.Instance.GetRelicById(relicId).iconOnMap;
        spriteRenderer.sprite = iconOnMap;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.name + "物体进入");
        if (Time.time - spawnTime < 0.5f)
            return;
        else
        {
            if (coll.gameObject.tag == "Player")
            {
                coll.transform.GetChild(0).GetComponent<PlayerInventory>().GetNewItem(relicId);
                Destroy(this.gameObject);
            }
        }
    }
}
