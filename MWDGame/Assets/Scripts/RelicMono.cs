using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class RelicMono : MonoBehaviour
{
    public List<RelicDetails> relicList;
    public int relicId;
    public SpriteRenderer spriteRenderer;
    public Sprite iconOnPlayer;
    public Sprite iconOnMap;

    public RelicDetails GetRelicById(int id) => relicList.FirstOrDefault(r => r.relicId == id);

    private void OnEnable()
    {
        iconOnPlayer = GetRelicById(relicId).iconOnPlayer;
        iconOnMap = GetRelicById(relicId).iconOnMap;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = iconOnMap;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            //TODO:碰到物体的玩家获得该物体
            //gameObject.();
        }
    }
}
