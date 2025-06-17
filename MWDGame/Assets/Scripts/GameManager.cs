using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerTextLeft;// ���� TextMeshPro ���
    public TextMeshProUGUI timerTextRight;// ���� TextMeshPro ���
    public float timeLeft = 60f;        // ��ʼ����ʱ 60 ��
    private bool countToZero;

    private bool isRunning = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        isRunning = true;

        while (timeLeft > 0)
        {
            timerTextLeft.text = Mathf.CeilToInt(timeLeft).ToString() + " ��";
            timerTextRight.text = Mathf.CeilToInt(timeLeft).ToString() + " ��";
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        timerTextLeft.text = "ʱ�䵽��";
        timerTextRight.text = "ʱ�䵽��";
        isRunning = false;
    }

    public void GameOver(int winner)
    {

    }
}
