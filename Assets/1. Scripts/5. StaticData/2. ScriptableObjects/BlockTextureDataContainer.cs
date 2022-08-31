using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    [CreateAssetMenu(fileName = "BlockTextureData", menuName = "StaticData/BlockTextureData")]
    public class BlockTextureDataContainer : ScriptableObject
    {
        [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
        public Material material;
        [VerticalGroup("Split/Properties")]
        public Vector2Int atlasIndexSize;
        [NonReorderable] public List<BlockTextureData> data;
    }

}


