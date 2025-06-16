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

    private void OnEnable()
    {
        iconOnPlayer = ResourceManager.Instance.GetRelicById(relicId).iconOnPlayer;
        iconOnMap = ResourceManager.Instance.GetRelicById(relicId).iconOnMap;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
