using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ����ʵ��
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI winDisplay;
    public TextMeshProUGUI timerTextLeft;
    public TextMeshProUGUI timerTextRight;
    public float timeLeft = 60f;
    public bool player1Win = true;

    private bool isRunning = false;

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
        GameCalculate();
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

    public void GameCalculate()
    {
        if (!isRunning)
        {
            winDisplay.text = player1Win ? "Player1 Win!" : "Player2 Win!";
            winDisplay.gameObject.SetActive(true);
        }
    }
}
