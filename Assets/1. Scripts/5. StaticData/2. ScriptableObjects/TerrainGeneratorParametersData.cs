using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.TerrainGenerator
{
    [CreateAssetMenu(fileName = "TerrainGeneratorParametersData", menuName = "StaticData/TerrainGeneratorParametersData")]
    public class TerrainGeneratorParametersData : ScriptableObject
    {
        [TabGroup("Generic Parameters")]
        [FormerlySerializedAs("mapSize")]
        public Vector2Int MapSize;
        
        [TabGroup("Generic Parameters")]
        [FormerlySerializedAs("chunkSize")] 
        public Vector3Int ChunkSize = new Vector3Int();

        [TabGroup("Generic Parameters")]
        public ChunkObject ChunkObject = null;
        
        [TabGroup("Generic Parameters")]
        [FormerlySerializedAs("blockSize")] 
        public float BlockSize = 1f;

        [TabGroup("Terrain Parameters")]
        [FormerlySerializedAs("terrainParameters")]
        public List<TerrainParameters> TerrainParameters;
        
        [TabGroup("Terrain Parameters")]
        [FormerlySerializedAs("heightModifier")]
        public float HeightModifier = 30f;
    }


}


