using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToNextScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Test3"; // 跳转的场景名称

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
