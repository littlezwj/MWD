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

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private ContactFilter2D movementFilter;
    private float collisionOffset = 0.05f;  // 碰撞缓冲

    private RaycastHit2D[] castResults = new RaycastHit2D[10];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Kinematic完全控制碰撞逻辑

        // 设置碰撞过滤器，默认检测所有Layer
        movementFilter = new ContactFilter2D();
        movementFilter.SetLayerMask(Physics2D.DefaultRaycastLayers);
        movementFilter.useTriggers = false;
    }

    void Update()
    {
        moveInput = GetInput();
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
        // 检测是否有碰撞
        int count = rb.Cast(direction, movementFilter, castResults, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            // 没有碰撞，正常移动
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // 有障碍，不移动
            // 滑动逻辑
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
}
