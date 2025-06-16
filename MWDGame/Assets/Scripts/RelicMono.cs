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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ResourceManager.Instance != null)
        {
            Debug.Log("ResourceManager.Instance �ѳɹ��ҵ���");
        }
        else
        {
            Debug.LogError("ResourceManager.Instance δ�ҵ�����ȷ�� ResourceManager �ű��ѹ��ز���ȷ���õ�����");
        }
        iconOnPlayer = ResourceManager.Instance.GetRelicById(relicId).iconOnPlayer;
        iconOnMap = ResourceManager.Instance.GetRelicById(relicId).iconOnMap;
        spriteRenderer.sprite = iconOnMap;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.name + "�������");
        if(coll.gameObject.tag == "Player")
        {
            //TODO:�����������һ�ø�����
            coll.transform.GetChild(0).GetComponent<PlayerInventory>().GetNewItem(relicId);
            Destroy(this.gameObject);
        }
    }
}
