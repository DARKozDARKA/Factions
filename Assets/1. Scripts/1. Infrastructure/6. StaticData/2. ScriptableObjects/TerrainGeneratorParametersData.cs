using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.TerrainGenerator
{
    [CreateAssetMenu(fileName = "TerrainGeneratorParametersData", menuName = "StaticData/TerrainGeneratorParametersData")]
    public class TerrainGeneratorParametersData : ScriptableObject
    {
        [FormerlySerializedAs("mapSize")]
        public Vector2Int MapSize;
        [FormerlySerializedAs("chunkSize")] 
        public Vector3Int ChunkSize = new Vector3Int();
        [FormerlySerializedAs("terrainParameters")]
        public List<TerrainParameters> TerrainParameters;
        public ChunkObject ChunkObject = null;
        [FormerlySerializedAs("blockSize")] 
        public float BlockSize = 1f;
        [FormerlySerializedAs("heightModifier")]
        public float HeightModifier = 30f;
    }


}


