using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    [System.Serializable]
    public class BlockTextureData
    {
        public BlockType type;
        public Vector2Int localIndexTop;
        public Vector2Int localIndexSide;
        public Vector2Int localIndexBottom;
    }
}


