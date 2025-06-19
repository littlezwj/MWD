using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveExpand : MonoBehaviour
{
    public float maxRadius = 5f;           // ������ɢ�뾶
    public float expandSpeed = 2f;         // ÿ��������ٰ뾶
    CircleCollider2D soundWaveColl;
    public float currentRadius;
    public float dizzyTime = 3f;
    public float baseRadius;
    private Vector3 originalScale;
    public PlayerController pc; //����ѣ�μ��ܵ�ʹ���߷�������
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
            Destroy(gameObject); // ��ɢ��ɺ�����
        }
    }
}
