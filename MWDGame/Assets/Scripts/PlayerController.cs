using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlScheme
    {
        WASD,
        ArrowKeys
    }

    [Header("控制设置")]
    public ControlScheme controlScheme = ControlScheme.WASD;
    public float moveSpeed = 5f;
    public GameObject shovelPrefab;  // 预制体：拖拽你的铲子预制体进来

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private ContactFilter2D movementFilter;
    private float collisionOffset = 0f;  // 碰撞缓冲
    private RaycastHit2D[] castResults = new RaycastHit2D[10];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Kinematic完全控制碰撞逻辑

        movementFilter = new ContactFilter2D();
        movementFilter.SetLayerMask(Physics2D.DefaultRaycastLayers);
        movementFilter.useTriggers = false;
    }

    void Update()
    {
        moveInput = GetInput();
        HandlePlaceShovel();  // 处理放置铲子
    }

    void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            TryMove(moveInput);
        }
    }

    private void TryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castResults, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // 有障碍时可以考虑加滑动逻辑（目前略）
        }
    }

    private Vector2 GetInput()
    {
        float h = 0f;
        float v = 0f;

        if (controlScheme == ControlScheme.WASD)
        {
            if (Input.GetKey(KeyCode.W)) v += 1f;
            if (Input.GetKey(KeyCode.S)) v -= 1f;
            if (Input.GetKey(KeyCode.A)) h -= 1f;
            if (Input.GetKey(KeyCode.D)) h += 1f;
        }
        else if (controlScheme == ControlScheme.ArrowKeys)
        {
            if (Input.GetKey(KeyCode.UpArrow)) v += 1f;
            if (Input.GetKey(KeyCode.DownArrow)) v -= 1f;
            if (Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) h += 1f;
        }

        return new Vector2(h, v).normalized;
    }

    private void HandlePlaceShovel()
    {
        if (controlScheme == ControlScheme.WASD)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaceShovel();
            }
        }
        else if (controlScheme == ControlScheme.ArrowKeys)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlaceShovel();
            }
        }
    }

    private void PlaceShovel()
    {
        if (shovelPrefab != null)
        {
            // 取整对齐（可选，避免放置在浮点偏移）
            Vector3 spawnPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
            Instantiate(shovelPrefab, spawnPos, Quaternion.identity);
        }
    }
}
