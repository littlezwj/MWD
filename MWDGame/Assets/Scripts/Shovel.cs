using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shovel : MonoBehaviour
{
    public float delayBeforeDig = 2f;
    public float digRadius = 1.1f;
    public LayerMask blockLayer;
    public GameObject[] itemPrefabs;

    void Start()
    {
        StartCoroutine(DigAfterDelay());
    }

    IEnumerator DigAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDig);
        Dig();
        Destroy(gameObject);
    }

    void Dig()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(digRadius*2, digRadius*2), 0f, blockLayer);
        foreach (Collider2D hit in hits)
        {
            Block block = hit.GetComponent<Block>();
            if (block != null && block.IsDestructible())
            {
                Destroy(hit.gameObject);
                TrySpawnItem(hit.transform.position);
            }
        }
    }

    void TrySpawnItem(Vector3 pos)
    {
        if (Random.value < 0.5f)
        {
            int randIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[randIndex], pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(digRadius*2, digRadius*2));
    }
}
