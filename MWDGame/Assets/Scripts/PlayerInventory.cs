using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
        }
        else
        {

        }
    }
}
