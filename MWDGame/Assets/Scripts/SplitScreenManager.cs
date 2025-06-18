using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{

    [Header("�������")]
    public Camera playerOneCamera;
    public Camera playerTwoCamera;
    public Transform playerOneCameraTransform;
    public Transform playerTwoCameraTransform;

    [Header("�������")]
    public CinemachineVirtualCamera vcamOne;
    public CinemachineVirtualCamera vcamTwo;

    [Header("���")]
    public Transform playerOne;
    public Transform playerTwo;

    [Header("��������")]
    public bool isHorizontalSplit = true; // falseΪ���·�����trueΪ���ҷ���

    // ����ʹ�õĲ����ƣ���Ҫ��Tags & Layers�����ȴ�����
    public string playerOneLayerName = "Player1Layer";
    public string playerTwoLayerName = "Player2Layer";

    void Start()
    {
        // ��֤����������ѷ���
        if (!playerOneCamera || !playerTwoCamera || !vcamOne || !vcamTwo)
        {
            Debug.LogError("��������б������������");
            return;
        }

        if (!playerOne || !playerTwo)
        {
            Debug.LogWarning("δ������Ҷ����޷����ø���Ŀ�꣡");
        }

        // ���÷����ӿ�
        SetupCameraViewports();

        // ������������ĸ���Ŀ��
        SetupVirtualCameras();

        // ���ò���޳�����
        SetupLayersAndMasks();

        // ȷ��ֻ��һ����Ƶ���������ڻ״̬
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
            // ���ҷ���
            playerOneCamera.rect = new Rect(0, 0, 0.5f, 1);
            playerTwoCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        else
        {
            // ���·���
            playerOneCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
            playerTwoCamera.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

    void SetupVirtualCameras()
    {
        if (playerOne)
        {
            vcamOne.Follow = playerOne;
            vcamOne.LookAt = playerOne; // ��ѡ
        }

        if (playerTwo)
        {
            vcamTwo.Follow = playerTwo;
            vcamTwo.LookAt = playerTwo; // ��ѡ
        }
    }

    void SetupLayersAndMasks()
    {
        // ��ȡ������
        int playerOneLayerIndex = LayerMask.NameToLayer(playerOneLayerName);
        int playerTwoLayerIndex = LayerMask.NameToLayer(playerTwoLayerName);

        if (playerOneLayerIndex == -1 || playerTwoLayerIndex == -1)
        {
            Debug.LogError("δ�ҵ�ָ���Ĳ㣡����Project Settings > Tags & Layers�д�����Щ�㡣");
            return;
        }

        // ������������Ĳ�
        vcamOne.gameObject.layer = playerOneLayerIndex;
        vcamTwo.gameObject.layer = playerTwoLayerIndex;

        // �����������ֻ��Ⱦ�ض��Ĳ�
        int defaultLayer = LayerMask.NameToLayer("Default");
        int obstacleLayer = LayerMask.NameToLayer("obstacleLayer");
        int uiLayer = LayerMask.NameToLayer("UI");
        int playerLayer = LayerMask.NameToLayer("PlayerLayer");
        playerOneCamera.cullingMask = (1 << playerLayer | 1 << playerOneLayerIndex) | (1 << defaultLayer) | (1 << obstacleLayer) | (1 << uiLayer);
        playerTwoCamera.cullingMask = (1 << playerLayer | 1 << playerTwoLayerIndex) | (1 << defaultLayer) | (1 << obstacleLayer) | (1 << uiLayer);
    }

    void DisableExtraAudioListeners()
    {
        // ȷ��ֻ��һ����Ƶ���������ڻ״̬
        if (playerOneCamera.GetComponent<AudioListener>() &&
            playerTwoCamera.GetComponent<AudioListener>())
        {
            playerTwoCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    // �л���������Ĺ������������Դ�UI�������ű����ã�
    public void ToggleSplitOrientation()
    {
        isHorizontalSplit = !isHorizontalSplit;
        SetupCameraViewports();
    }
}
