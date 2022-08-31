using System;
using UnityEngine;

public abstract class NatureData : ScriptableObject
{
    public string TreeName;
    public GameObject Prefab;
    public Vector3Int Size;
    [Range(0f, 1f)] public float Rarity = 0.5f;
    public RotationType RotationType;
    [NonSerialized] public Vector2 Range;
}