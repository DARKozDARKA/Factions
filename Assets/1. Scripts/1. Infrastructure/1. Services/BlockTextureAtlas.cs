using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infastructure;

namespace CodeBase.TerrainGenerator
{
    public class BlockTextureAtlas
    {
        private readonly Dictionary<BlockType, BlockTexture> _typeToTexture = new Dictionary<BlockType, BlockTexture>();
        private readonly Material _material;

        public BlockTextureAtlas(IStaticDataService dataService)
        {
            BlockTextureDataContainer container = dataService.GetTextureContainer();
            _material = container.material;

            CreateAtlasDictionary(container);
        }

        public BlockTexture GetBlockTexture(BlockType type)
        {
            return _typeToTexture[type];
        }

        public Vector2? GetQuadTexture(BlockType type, QuadType quadType)
        {
            BlockTexture blockTexture = _typeToTexture[type];
            return SwitchQuadTexture(quadType, blockTexture);

        }

        public Material GetMaterial()
        {
            return _material;
        }

        private void CreateAtlasDictionary(BlockTextureDataContainer container)
        {
            foreach (var item in container.data)
            {
                BlockType type = item.type;
                BlockTexture blockTexture = new BlockTexture()
                    .With(_ => _.top = (Vector2)item.localIndexTop / (Vector2)container.atlasIndexSize)
                    .With(_ => _.side = (Vector2)item.localIndexSide / (Vector2)container.atlasIndexSize)
                    .With(_ => _.bottom = (Vector2)item.localIndexBottom / (Vector2)container.atlasIndexSize)
                    .With(_ => _.size = new Vector2(1f / (float)container.atlasIndexSize.x, 1f / (float)container.atlasIndexSize.y));
                _typeToTexture.Add(type, blockTexture);
            }
        }

        private Vector2? SwitchQuadTexture(QuadType quadType, BlockTexture blockTexture)
        {
            return quadType switch
            {
                QuadType.Back => blockTexture.side,
                QuadType.Front => blockTexture.side,
                QuadType.Left => blockTexture.side,
                QuadType.Right => blockTexture.side,
                QuadType.Top => blockTexture.top,
                QuadType.Bottom => blockTexture.bottom,
                _ => null
            };
        }
    }
}


