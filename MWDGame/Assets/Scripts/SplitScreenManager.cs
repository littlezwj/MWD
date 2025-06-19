using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{

    [Header("物理相机")]
    public Camera playerOneCamera;
    public Camera playerTwoCamera;
    public Transform playerOneCameraTransform;
    public Transform playerTwoCameraTransform;

    [Header("虚拟相机")]
    public CinemachineVirtualCamera vcamOne;
    public CinemachineVirtualCamera vcamTwo;

    [Header("玩家")]
    public Transform playerOne;
    public Transform playerTwo;

    [Header("分屏设置")]
    public bool isHorizontalSplit = true; // false为上下分屏，true为左右分屏

    // 分屏使用的层名称（需要在Tags & Layers中事先创建）
    public string playerOneLayerName = "Player1Layer";
    public string playerTwoLayerName = "Player2Layer";

    void Start()
    {
        // 验证所有组件都已分配
        if (!playerOneCamera || !playerTwoCamera || !vcamOne || !vcamTwo)
        {
            Debug.LogError("请分配所有必需的相机组件！");
            return;
        }

        if (!playerOne || !playerTwo)
        {
            Debug.LogWarning("未分配玩家对象，无法设置跟踪目标！");
        }

        // 设置分屏视口
        SetupCameraViewports();

        // 设置虚拟相机的跟踪目标
        SetupVirtualCameras();

        // 设置层和剔除遮罩
        SetupLayersAndMasks();

        // 确保只有一个音频监听器处于活动状态
        DisableExtraAudioListeners();

        playerOneCameraTransform = playerOneCamera.transform;
        playerTwoCameraTransform = playerTwoCamera.transform;
    }

    /*
    private void Update()
    {
        if(playerOneCameraTransform.rotation.x != 0 || playerOneCameraTransform.rotation.y != 0 || playerTwoCameraTransform.rotation.x != 0 || playerTwoCameraTransform.rotation.y != 0)
        {
            playerOneCameraTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }*/

    void SetupCameraViewports()
    {
        if (isHorizontalSplit)
        {
            // 左右分屏
            playerOneCamera.rect = new Rect(0, 0, 0.5f, 1);
            playerTwoCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        else
        {
            // 上下分屏
            playerOneCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
            playerTwoCamera.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

    void SetupVirtualCameras()
    {
        if (playerOne)
        {
            vcamOne.Follow = playerOne;
            vcamOne.LookAt = playerOne; // 可选
        }

        if (playerTwo)
        {
            vcamTwo.Follow = playerTwo;
            vcamTwo.LookAt = playerTwo; // 可选
        }
    }

    void SetupLayersAndMasks()
    {
        // 获取层索引
        int playerOneLayerIndex = LayerMask.NameToLayer(playerOneLayerName);
        int playerTwoLayerIndex = LayerMask.NameToLayer(playerTwoLayerName);

        if (playerOneLayerIndex == -1 || playerTwoLayerIndex == -1)
        {
            Debug.LogError("未找到指定的层！请在Project Settings > Tags & Layers中创建这些层。");
            return;
        }

        // 设置虚拟相机的层
        vcamOne.gameObject.layer = playerOneLayerIndex;
        vcamTwo.gameObject.layer = playerTwoLayerIndex;

        // 设置物理相机只渲染特定的层
        int defaultLayer = LayerMask.NameToLayer("Default");
        int obstacleLayer = LayerMask.NameToLayer("obstacleLayer");
        int uiLayer = LayerMask.NameToLayer("UI");
        int playerLayer = LayerMask.NameToLayer("PlayerLayer");
        playerOneCamera.cullingMask = (1 << playerLayer | 1 << playerOneLayerIndex) | (1 << defaultLayer) | (1 << obstacleLayer) | (1 << uiLayer);
        playerTwoCamera.cullingMask = (1 << playerLayer | 1 << playerTwoLayerIndex) | (1 << defaultLayer) | (1 << obstacleLayer) | (1 << uiLayer);
    }

    void DisableExtraAudioListeners()
    {
        // 确保只有一个音频监听器处于活动状态
        if (playerOneCamera.GetComponent<AudioListener>() &&
            playerTwoCamera.GetComponent<AudioListener>())
        {
            playerTwoCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    // 切换分屏方向的公共方法（可以从UI或其他脚本调用）
    public void ToggleSplitOrientation()
    {
        isHorizontalSplit = !isHorizontalSplit;
        SetupCameraViewports();
    }
}
