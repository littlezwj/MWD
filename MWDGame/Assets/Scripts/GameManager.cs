using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerTextLeft;// 拖入 TextMeshPro 组件
    public TextMeshProUGUI timerTextRight;// 拖入 TextMeshPro 组件
    public float timeLeft = 60f;        // 初始倒计时 60 秒
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
            timerTextLeft.text = Mathf.CeilToInt(timeLeft).ToString() + " 秒";
            timerTextRight.text = Mathf.CeilToInt(timeLeft).ToString() + " 秒";
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        timerTextLeft.text = "时间到！";
        timerTextRight.text = "时间到！";
        isRunning = false;
    }

    public void GameOver(int winner)
    {

    }
}
