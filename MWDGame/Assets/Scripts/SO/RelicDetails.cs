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
    ����������,
    ���Ժڽ�Ž�,
    ���в�,
    ��׻���,
    ��Ĺ��,
    �����޼ϻ,
}