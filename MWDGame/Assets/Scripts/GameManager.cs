using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ����ʵ��
    public static GameManager Instance { get; private set; }

    public Text winDisplay;
    public Text timerTextLeft;
    public Text timerTextRight;
    public float timeLeft = 60f;
    public bool player1Win = true;

    private bool isRunning = false;
    public Text player1Prompt;
    public Text player2Prompt;

    private void Awake()
    {
        // ʵ�ֵ���
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // �����ϣ���ڳ����л�ʱ���ָö������
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if (!isRunning)
        {
            GameCalculate(false);
        }
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

    public void GameCalculate(bool player1Win)
    {
            winDisplay.text = player1Win ? "��Ĺ�˻�ʤ��" : "�ػ��˻�ʤ��";
            winDisplay.gameObject.SetActive(true);
    }

    public void GameProcess()
    {
        player1Prompt.gameObject.SetActive(true);
        player2Prompt.gameObject.SetActive(true);
        Invoke("MessageOver", 3.0f);
    }

    public void MessageOver()
    {
        player1Prompt.gameObject.SetActive(false);
        player2Prompt.gameObject.SetActive(false);
    }
}
