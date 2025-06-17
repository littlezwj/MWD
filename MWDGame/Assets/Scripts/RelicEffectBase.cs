using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicType
{
    Addition,
    Shoot,
    BoomerRelative,
    CubeRelative,
}

public class RelicEffectBase : MonoBehaviour
{
    public RelicType relicType;

    public virtual void RelicUse()
    {
        Debug.Log(this.gameObject.name + "正在被使用!");
    }
}
