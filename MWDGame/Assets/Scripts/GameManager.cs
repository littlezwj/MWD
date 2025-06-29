using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 单例实例
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
        if (!isRunning)
        {
            GameCalculate(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
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

    public void GameCalculate(bool player1Win)
    {
            winDisplay.text = player1Win ? "盗墓人获胜！" : "守护人获胜！";
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
    void QuitGame()
    {
#if UNITY_EDITOR
        // 编辑器中退出运行
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 打包后的游戏退出
            Application.Quit();
#endif
    }
}
