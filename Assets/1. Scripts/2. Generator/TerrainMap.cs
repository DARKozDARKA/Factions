using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    // Warning
    // Technical code
    // Warning
    public class TerrainMap
    {
        public readonly ChunkObject[,] chunks;
        public readonly Vector2Int chunkSize;
        public readonly Vector2Int mapSize;

        private Dictionary<StructureType, List<Structure>> _typeToStructure =
            new Dictionary<StructureType, List<Structure>>();

        public TerrainMap(ChunkObject[,] chunks, Vector2Int chunkSize, Vector2Int mapSize)
        {
            this.chunks = chunks;
            this.chunkSize = chunkSize;
            this.mapSize = mapSize;
        }

        public TerrainBlock GetSurfaceBlock(Vector2Int position)
        {
            var chunkPosition = new Vector2Int(
                Mathf.FloorToInt(position.x / (float)chunkSize.x),
                Mathf.FloorToInt(position.y / (float)chunkSize.y));

            var blockPosition = new Vector2Int(
                position.x - (chunkPosition.x * chunkSize.x),
                position.y - (chunkPosition.y * chunkSize.y));

            if (!chunks.In2DArrayBounds(chunkPosition)) return null;
            var chunk = chunks[chunkPosition.x, chunkPosition.y];
            if (!chunk.cubeobjects.In3DArrayBounds(blockPosition.x, 0, blockPosition.y)) return null;
            var surfaceBlockIndex = chunk.heightMap[blockPosition.x, blockPosition.y] - 1;

            return chunk.cubeobjects[blockPosition.x, surfaceBlockIndex, blockPosition.y];
        }

        public List<TerrainBlock> GetRect(Vector2Int position, Vector2Int size)
        {
            var blocks = new List<TerrainBlock>();
            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    var newBlock = GetSurfaceBlock(new Vector2Int(x, y));
                    if (newBlock == null)
                        return null;
                    blocks.Add(newBlock);
                }
            }

            return blocks;
        }

        public bool CheckCleanRect(Vector2Int position, Vector2Int size)
        {
            List<TerrainBlock> blocks = GetRect(position, size);

            if (blocks == null)
                return false;

            List<int> blocksHeight = blocks.Select(_ => _.surfaceHeight).ToList();

            if (blocks.Any(_ => _.isOccupied))
                return false;

            if (blocksHeight.Distinct().Count() != 1)
                return false;

            blocksHeight.Clear();

            return true;
        }

        public void OccupieRect(Vector2Int position, Vector2Int size, StructureType structureType = StructureType.Null)
        {
            List<TerrainBlock> blocks = GetRect(position, size);
            blocks.All(_ => _.isOccupied = true);
            blocks.All(_ =>
            {
                _.StructureType = structureType;
                return true;
            });

            var newStructure = new Structure()
                .With(_ => _.Center = position)
                .With(_ => _.Type = structureType)
                .With(_ => _.TerrainBlocks = blocks);

            if (_typeToStructure.ContainsKey(structureType))
                _typeToStructure[structureType].Add(newStructure);
            else
                _typeToStructure[structureType] = new List<Structure>() { newStructure };
        }

        public Structure GetClosestStructure(Vector2Int position, StructureType structureType)
        {
            if (!_typeToStructure.ContainsKey(structureType)) return null;

            Structure closestStructure = _typeToStructure[structureType][0];
            float closestStructureMagnitude = (closestStructure.Center - position).magnitude;
            foreach (var structure in _typeToStructure[structureType])
            {
                float newMagnitude = (structure.Center - position).magnitude;
                if (newMagnitude <= closestStructureMagnitude)
                    continue;

                closestStructure = structure;
                closestStructureMagnitude = newMagnitude;
            }

            return closestStructure;
        }
    }
}