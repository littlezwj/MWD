using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public Image relicImage;
    private SpriteRenderer sr;
    public bool isEquipped = false;
    public int relicId;
    public bool itemIsUsable;
    public int importantRelicID;
    public GameObject relicStandard;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void GetNewItem(int itemId)
    {
        if (importantRelicID != 0)
        {
            GameObject importantRelic = Instantiate(relicStandard, transform.parent.position, Quaternion.identity);
            importantRelic.GetComponent<RelicMono>().relicId = importantRelicID;
            importantRelic.GetComponent<RelicMono>().spawnTime = Time.time;
            importantRelicID = 0;
        }
        relicId = itemId;
        RelicDetails relicHolding = ResourceManager.Instance.GetRelicById(itemId);
        if (relicHolding.isImportant)
        {
            importantRelicID = relicId;
        }
        if (!isEquipped)
        {
            isEquipped = true;
            sr.enabled = true;
            sr.sprite = relicHolding.iconOnPlayer;
            relicImage.enabled = true;
            relicImage.sprite = relicHolding.iconOnPlayer;
            relicImage.transform.GetChild(0).GetComponent<Text>().text = relicHolding.relicName;
            relicImage.transform.GetChild(1).GetComponent<Text>().text = relicHolding.relicDescription;
            itemIsUsable = relicHolding.isUsable;
        }
        else
        {
            //TODO:��ұ��������е��ߵ�ǰ���»���µĵ���
            sr.enabled = true;
            relicImage.enabled = true;
            relicImage.sprite = relicHolding.iconOnPlayer;
            relicImage.transform.GetChild(0).GetComponent<Text>().text = relicHolding.relicName;
            relicImage.transform.GetChild(1).GetComponent<Text>().text = relicHolding.relicDescription;
            itemIsUsable = relicHolding.isUsable;
        }
    }

    public void ResetInvetory()
    {
        if(itemIsUsable)
        {
            isEquipped = false;
            relicId = 0;
            sr.sprite = null;
            sr.enabled = false;
            relicImage.enabled = false;
            relicImage.sprite = null;
            relicImage.transform.GetChild(0).GetComponent<Text>().text = " ";
            relicImage.transform.GetChild(1).GetComponent<Text>().text = " ";
        }
    }
}
