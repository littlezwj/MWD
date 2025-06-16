using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRelicDetails", menuName = "Relic/RelicDetails")]
public class RelicDetails : ScriptableObject
{
    public int relicId;
    public RelicName relicName;
    public Sprite iconOnPlayer;     
    public Sprite iconOnMap;
    public RelicEffectBase relicEffect;
    public bool isImportant;
}

public enum RelicName
{
    None,
    ²âÊÔÂåÑô²ù,
    ²âÊÔºÚ½ğ¹Å½£,
    ÌúÈĞ²ù,
    Æá¹×»¬ÂÄ,
    ÕòÄ¹·û,
    åó»úİğŞ¼Ï»,
}