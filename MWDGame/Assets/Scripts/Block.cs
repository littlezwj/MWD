using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType { A, B }
    public BlockType blockType;

    public bool IsDestructible()
    {
        return blockType == BlockType.A;
    }
}
