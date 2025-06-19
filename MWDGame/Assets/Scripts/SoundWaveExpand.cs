using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveExpand : MonoBehaviour
{
    public float maxRadius = 5f;           // 最终扩散半径
    public float expandSpeed = 2f;         // 每秒扩大多少半径
    CircleCollider2D soundWaveColl;
    public float currentRadius;
    public float dizzyTime = 3f;
    public float baseRadius;
    private Vector3 originalScale;
    public PlayerController pc; //传递眩晕技能的使用者方便重置
    void Start()
    {
        soundWaveColl = GetComponent<CircleCollider2D>();
        baseRadius = soundWaveColl.radius;
        currentRadius = baseRadius;
        originalScale = transform.localScale;
    }


    void Update()
    {
        if (currentRadius < maxRadius)
        {
            currentRadius += expandSpeed * Time.deltaTime;
            //soundWaveColl.radius = currentRadius;
            float scaleFactor = currentRadius / baseRadius;
            transform.localScale = originalScale * scaleFactor;
        }
        else
        {
            pc.isDizzyUser = false;
            Destroy(gameObject); // 扩散完成后销毁
        }
    }
}
