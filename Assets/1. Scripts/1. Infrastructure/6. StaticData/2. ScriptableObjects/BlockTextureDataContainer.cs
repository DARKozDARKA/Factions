using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    [CreateAssetMenu(fileName = "BlockTextureData", menuName = "StaticData/BlockTextureData")]
    public class BlockTextureDataContainer : ScriptableObject
    {
        public Material material;
        public Vector2Int atlasIndexSize;
        [NonReorderable] public List<BlockTextureData> data;
    }

}


