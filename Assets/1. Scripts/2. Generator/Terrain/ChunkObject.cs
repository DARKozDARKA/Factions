using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace CodeBase.TerrainGenerator
{
    public class ChunkObject
    {
        public readonly TerrainBlock[,,] cubeobjects = null;
        public readonly int[,] heightMap;
        public readonly bool allowedToBuild = true;
        public readonly int sizeX = 0;
        public readonly int sizeY = 0;
        public readonly int sizeZ = 0;
        public readonly List<GameObject> chunkObjects;

        private const float Offset = 1.0f;

        private readonly List<TerrainParameters> parameters = null;
        private Vector2Int _mapSize;
        public Vector2Int chunkOffset => _offset;
        private Vector2Int _offset;
        private float _heightModifier;

        public ChunkObject(ChunkParameters parameters)
        {
            sizeX = parameters.chunkSize.x;
            sizeY = parameters.chunkSize.y;
            sizeZ = parameters.chunkSize.z;

            _mapSize = parameters.mapSize; ;
            _offset = parameters.offset;
            _heightModifier = parameters.heightModifier;

            this.parameters = new List<TerrainParameters>(parameters.terrainParameters);
            cubeobjects = new TerrainBlock[sizeX, sizeY, sizeZ];
            heightMap = new int[sizeX, sizeZ];
            chunkObjects = new List<GameObject>();

            InitCubes();
        }
        private void InitCubes()
        {
            for (int z = 0; z < sizeZ; z++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    float height = 0f;
                    for (int pl = 0; pl < parameters.Count; pl++)
                    {
                        float xval = (((float)x + (float)_offset.x * (float)sizeX) / ((float)sizeX * (float)_mapSize.x));
                        float zval = (((float)z + (float)_offset.y * (float)sizeZ) / ((float)sizeZ * (float)_mapSize.y));
                        float perlin = Mathf.PerlinNoise(xval * parameters[pl].perlinScale, zval * parameters[pl].perlinScale);
                        height += Mathf.RoundToInt(perlin * parameters[pl].terrainHeight * _heightModifier);
                    }

                    for (int i = 0; i < sizeY; i++)
                    {
                        cubeobjects[x, i, z] = new TerrainBlock()
                            .With(_ => _.isVisible = (i < height))
                            .With(_ => _.type = BlockType.Dirt)
                            .With(_ => _.surfaceHeight = (int)height);
                        heightMap[x, z] = (int)height;

                    }
                }
            }
        }
    }
}


