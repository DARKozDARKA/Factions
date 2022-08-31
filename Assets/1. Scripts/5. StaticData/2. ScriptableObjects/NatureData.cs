using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class NatureData : ScriptableObject
{
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public GameObject Prefab;
    
    [VerticalGroup("Split/Properties")]
    public Vector3Int Size;
    
    [VerticalGroup("Split/Properties")]
    [Range(0f, 1f)]
    public float Rarity = 0.5f;
    
    [VerticalGroup("Split/Properties")]
    public RotationType RotationType;
    
    [NonSerialized]
    [VerticalGroup("Split/Properties")]
    public Vector2 Range;
}