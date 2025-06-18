using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public enum Direction
{
    Up,
    Right,
    Left,
    Down
}*/

public class ArrowMove : MonoBehaviour
{
    /*    
    Dictionary<Direction, Vector2> directionMap = new Dictionary<Direction, Vector2>
    {
        { Direction.Up,    new Vector2(0, 1) },
        { Direction.Right, new Vector2(1, 0) },
        { Direction.Left,  new Vector2(-1, 0) },
        { Direction.Down,  new Vector2(0, -1) }
    };*/
    private Rigidbody2D rb;
    public float speed = 1f;
    //public Direction direction;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = directionMap[direction] * speed;
    }
}
