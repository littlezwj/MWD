using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRelicDetails", menuName = "Relic/RelicDetails")]
public class RelicDetails : ScriptableObject
{
    public int relicId;
    public string relicName;
    public Sprite iconOnPlayer;     
    public Sprite iconOnMap;
    //public RelicEffectBase relicEffect;
    public bool isImportant;
    public bool isUsable;
    public string relicDescription;
}
