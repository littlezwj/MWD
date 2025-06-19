using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player1")
        {
            collision.gameObject.transform.GetChild(0).GetComponent<PlayerInventory>().GetNewItem(100);
            GameManager.Instance.GameProcess();
            Destroy(this.gameObject);
        }
    }
}
