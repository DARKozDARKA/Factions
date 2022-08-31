using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    [System.Serializable]
    public class TerrainParameters
    {
        [Range(0.1f, 5.0f)]
        public float perlinScale;
        [Range(0.1f, 1f)]
        public float terrainHeight;
        [HideInInspector]
        public float xPerlinOffset;
        [HideInInspector]
        public float zPerlinOffset;
    }

}

