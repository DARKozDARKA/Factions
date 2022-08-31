using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    public class ChunkParameters
    {
        public Vector3Int chunkSize;
        public List<TerrainParameters> terrainParameters;
        public Vector2Int mapSize;
        public Vector2Int offset;
        public float heightModifier;
    }
}


