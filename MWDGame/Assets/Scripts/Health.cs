using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject healthUI; // 拖入 UI 父物体
    private TMP_Text healthText;
    private SpriteRenderer dizzySprite;

    public float respawnDelay = 3f;
    private Vector3 spawnPosition;
    public bool holdingFeiyi = false;
    public PlayerController playerController;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnPosition = transform.position;
        healthText = healthUI.GetComponentInChildren<TMP_Text>();
        dizzySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        playerController = gameObject.GetComponent<PlayerController>();
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            if(!holdingFeiyi)  DieAndRespawn();
            else
            {
                currentHealth++;
                UpdateHealthUI();
                holdingFeiyi = false;
                transform.GetChild(0).GetComponent<PlayerInventory>().itemIsUsable = true;
                transform.GetChild(0).GetComponent<PlayerInventory>().ResetInvetory();
            }
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
        if(dizzySprite != null)
        {
            dizzySprite.gameObject.SetActive(playerController.dizzy);
        }
    }

    void DieAndRespawn()
    {
        //Debug.Log($"{gameObject.name} died!");
        gameObject.SetActive(false); // 暂时关闭角色
        Invoke(nameof(Respawn), respawnDelay);
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        transform.position = spawnPosition; // 回到出生点
        gameObject.SetActive(true); // 重新激活
        //Debug.Log($"{gameObject.name} respawned!");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SoundWave" && !playerController.isDizzyUser)
        {
            playerController.dizzy = true;
            UpdateHealthUI();
            Invoke("DizzyOver", collision.GetComponent<SoundWaveExpand>().dizzyTime);
        }
    }

    private void DizzyOver()
    {
        playerController.dizzy = false;
        UpdateHealthUI();
    }

}
