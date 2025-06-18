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

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void GetNewItem(int itemId)
    {
        relicId = itemId;
        if (!isEquipped)
        {
            isEquipped = true;
            sr.sprite = ResourceManager.Instance.GetRelicById(itemId).iconOnPlayer;
            relicImage.sprite = ResourceManager.Instance.GetRelicById(itemId).iconOnPlayer;
            relicImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.GetRelicById(itemId).relicName;
            relicImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.GetRelicById(itemId).relicDescription;
        }
        else
        {
            //TODO:玩家背包中已有道具的前提下获得新的道具
        }
    }

    public void ResetInvetory()
    {
        isEquipped = false;
        relicId = 0;
        relicImage.sprite = null;
        relicImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " ";
        relicImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " ";
    }
}
