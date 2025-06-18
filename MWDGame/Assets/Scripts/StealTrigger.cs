using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class StealTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player1")
        {
            PlayerInventory inventory = coll.gameObject.transform.GetChild(0).GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                Debug.Log("Player1Inventory进入！");
                bool getArtifact = ResourceManager.Instance.GetRelicById(inventory.relicId).isImportant;
                if (getArtifact)
                {
                    GameManager.Instance.GameCalculate(true);
                }
            }
            else
            {
                Debug.Log("未找到对应的PlayerInventory组件!");
            }
        }
    }
}
