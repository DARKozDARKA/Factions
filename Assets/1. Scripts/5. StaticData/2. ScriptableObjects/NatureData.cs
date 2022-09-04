using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class NatureData : ScriptableObject
{
    public AssetReferenceGameObject Prefab;
    
    public Vector3Int Size;
    
    [Range(0f, 1f)]
    public float Rarity = 0.5f;
    
    public RotationType RotationType;
    
    [NonSerialized]
    public Vector2 Range;
}