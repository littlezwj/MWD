using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 单例实例
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI winDisplay;
    public TextMeshProUGUI timerTextLeft;
    public TextMeshProUGUI timerTextRight;
    public float timeLeft = 60f;
    public bool player1Win = true;

    private bool isRunning = false;

    private void Awake()
    {
        // 实现单例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // 如果你希望在场景切换时保持该对象存在
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
            timerTextLeft.text = Mathf.CeilToInt(timeLeft).ToString() + " 秒";
            timerTextRight.text = Mathf.CeilToInt(timeLeft).ToString() + " 秒";
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        timerTextLeft.text = "时间到！";
        timerTextRight.text = "时间到！";
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
